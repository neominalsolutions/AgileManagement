using AgileManagement.Application;
using AgileManagement.Application.validators;
using AgileManagement.Core;
using AgileManagement.Core.data;
using AgileManagement.Core.validation;
using AgileManagement.Domain;
using AgileManagement.Domain.repositories;
using AgileManagement.Infrastructure.events;
using AgileManagement.Infrastructure.notification.smtp;
using AgileManagement.Infrastructure.security.hash;
using AgileManagement.Persistence.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddControllersWithViews();
            services.AddSingleton<IEmailService, NetSmtpEmailService>();
            services.AddTransient<IUserRegisterValidator, UserRegisterValidator>();
            services.AddSingleton<IPasswordHasher, CustomPasswordHashService>();
            services.AddScoped<IUserRegisterService, UserRegisterService>();
            services.AddScoped<IUserDomainService, UserDomainService>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            // best practice olarak db context uygyulamasý appsettings dosyasýndan bilgileri conectionstrings node dan alýrýz.


            services.AddSingleton<IDomainEventHandler<UserCreatedEvent>, UserCreatedHandler>();
            services.AddSingleton<IDomainEventDispatcher, NetCoreEventDispatcher>();





            services.AddDbContext<UserDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("LocalDb"));
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
