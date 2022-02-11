using AgileManagement.Application;
using AgileManagement.Core;
using AgileManagement.Core.validation;
using AgileManagement.Domain;
using AgileManagement.Infrastructure;
using AgileManagement.Mvc.Services;
using AgileManagement.Persistence.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddHttpContextAccessor(); // IHttpContext Accessor
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDataProtection(); // Uygulamada dataProtection özelliði kullanacaðým.

            // Mvc uygulamasýnda automapper kullanacaðýmýzý söyledik
            services.AddAutoMapper(typeof(Startup));

            // session bazlý oturum bazlý olacaðýnda singleton kesinlikle kullanmayýn. her oturum açan kiþi için deðiþecek bu sýnýf
            // scope bazlý olmasýnýna da gerek yok. herhangi bir external iþlem yapmýyoruz.
            services.AddTransient<AuthenticatedUser>();

            DomainModule.Load(services);
            InfrastructureModule.Load(services);
            ApplicationModule.Load(services);
            EFModule.Load(services, Configuration);


            services.AddAuthentication("NormalScheme").AddCookie("NormalScheme", opt =>
             {

                 opt.Cookie.HttpOnly = false; // https bir cookie ile cookie https protocolü ile çalýþsýn
                 opt.Cookie.Name = "NormalCookie";
                 opt.LoginPath = "/Account/Login";
                 opt.LogoutPath = "/Account/Logout";
                 opt.AccessDeniedPath = "/Account/AccessDenied"; // yetkiniz olmayan sayfalar.
                 opt.SlidingExpiration = true; // otomatik olarak cookie yenileme, süresini kaydýrarak expire time yeniden 30 gün sonrasýna atar.
                                               // cookie expire olunca tekrar login olmamýz gerekiyor.

             });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // bunun yeri öenmli UseRouting ile UseAuthorization arasýna konumlandýralým.
            app.UseAuthentication(); // sistemde kimlik doðrulamasý var kullanýcýnýn hesabýný cookie üzerinden kontrol ederiz.
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Project}/{action=Management}/{id?}"
          );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
