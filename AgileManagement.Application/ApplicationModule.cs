using AgileManagement.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    public static class ApplicationModule
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<IUserRegisterValidator, UserRegisterValidator>();
            // validation, session işlemleri için transient tercih edelim

            services.AddScoped<ICookieAuthenticationService, CookieAuthenticationService>();
            services.AddScoped<IUserRegisterService, UserRegisterService>();
            services.AddScoped<IAccountVerifyService, AccountVerifyService>();
            services.AddScoped<IUserLoginService, UserLoginService>();

            services.AddScoped<IProjectWithContributorsRequestService, ProjectWithContributorsRequestService>();
            services.AddScoped<IContributorProjectAccessApprovementService, ContributorProjectAccessApprovementService>();
        }
    }
}
