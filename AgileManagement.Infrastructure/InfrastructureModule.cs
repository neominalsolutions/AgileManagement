using AgileManagement.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void Load(IServiceCollection services)
        {

            services.AddSingleton<IDomainEventDispatcher, NetCoreEventDispatcher>();
            // konfigürasyon, yardımcı servis gibi tek instance ile çalışabilen yapılar için singleton tercih edelim
            services.AddSingleton<IEmailService, NetSmtpEmailService>();
            // veri tabanı , servis çağırısı, api çağırısı gibi işlemler için scoped tercih edelim
            services.AddSingleton<IPasswordHasher, CustomPasswordHashService>();
        }
    }
}
