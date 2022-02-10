using AgileManagement.Core;
using AgileManagement.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Infrastructure
{
    public class NetCoreEventDispatcher : IDomainEventDispatcher
    {
        /// <summary>
        /// Net Coreda service provider ile addServices olarak startup'a tanımlanmış olan bir Handler'a _serviceProvider.GetService ile erişmemiz lazım ki doğru handler'ın handle methodunu tetikleyelim. Sistem buna çalışma zamanında karar verecektir. Biz bu tasarım desenine Service Locator Pattern ismini veriyoruz.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        public NetCoreEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public void Dispatch<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            if (_serviceProvider != null)
            {

                using (var scope = _serviceProvider.CreateScope())
                {

                    foreach (var handler in scope.ServiceProvider.GetServices<IDomainEventHandler<TDomainEvent>>())
                    {
                        
                        handler.Handle(@event);
                    }
                }
            }

        }


    }
}
