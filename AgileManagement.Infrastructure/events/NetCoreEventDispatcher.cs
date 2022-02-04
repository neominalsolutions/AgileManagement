using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Infrastructure.events
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

        public void Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {

            // Reflection ile çalışma zamanında hangi interfaceden türüyen servisin çalışacağını bulduk
            var handler = _serviceProvider.GetService(typeof(IDomainEventHandler<TEvent>));
            // UserCreatedHandler
            ((dynamic)handler).Handle(@event);
        }
    }
}
