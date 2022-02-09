using AgileManagement.Application;
using AgileManagement.Application.services;
using AgileManagement.Application.validators;
using AgileManagement.Core;
using AgileManagement.Core.validation;
using AgileManagement.Domain;
using AgileManagement.Domain.events;
using AgileManagement.Domain.handler;
using AgileManagement.Domain.repositories;
using AgileManagement.Infrastructure.events;
using AgileManagement.Infrastructure.notification.smtp;
using AgileManagement.Infrastructure.security.hash;
using AgileManagement.Persistence.EF;
using AgileManagement.Persistence.EF.repositories;
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
            services.AddDataProtection(); // Uygulamada dataProtection özelliði kullanacaðým.

            // Mvc uygulamasýnda automapper kullanacaðýmýzý söyledik
            services.AddAutoMapper(typeof(Startup));

            // konfigürasyon, yardýmcý servis gibi tek instance ile çalýþabilen yapýlar için singleton tercih edelim
            services.AddSingleton<IEmailService, NetSmtpEmailService>();
            services.AddTransient<IUserRegisterValidator, UserRegisterValidator>();
            // validation, session iþlemleri için transient tercih edelim
           

            // veri tabaný , servis çaðýrýsý, api çaðýrýsý gibi iþlemler için scoped tercih edelim
            services.AddSingleton<IPasswordHasher, CustomPasswordHashService>();
            services.AddScoped<ICookieAuthenticationService, CookieAuthenticationService>();
            services.AddScoped<IUserRegisterService, UserRegisterService>();
            services.AddScoped<IAccountVerifyService, AccountVerifyService>();
            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddScoped<IUserDomainService, UserDomainService>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IProjectRepository, EFProjectRepository>();
            // best practice olarak db context uygyulamasý appsettings dosyasýndan bilgileri conectionstrings node dan alýrýz.



            //services.AddSingleton<IDomainEventDispatcher, NetCoreEventDispatcher>();


            //services.AddAuthentication("SecureScheme").AddCookie("SecureScheme", opt =>
            //{
            //    opt.Cookie.HttpOnly = false; // https bir cookie ile cookie https protocolü ile çalýþsýn
            //    opt.Cookie.Name = "AdminCookie";
            //    opt.ExpireTimeSpan = TimeSpan.FromDays(1); // 1 günlük olarak cookie browserdan silinmeyecek
            //    opt.LoginPath = "/Admin/Accoun/Login";
            //    opt.LogoutPath = "/Admin/Account/Logout";
            //    opt.AccessDeniedPath = "/Admin/Account/AccessDenied"; // yetkiniz olmayan sayfalar.
            //});

            // NormalAuth bizim uygulamdaki normal kullanýcýlar için açtýðýmýz kimlik doðrulama þemasýdýr.
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


          

            // Yönetim paneline giriþ yetkisi olan kullanýlar için olucak olan cookie




            services.AddDbContext<UserDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("LocalDb"));
            });

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("LocalDb"));
            });


            IKernel kernel = new StandardKernel();
            NinjectEventModule.RegisterServices(kernel);

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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
