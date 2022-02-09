using AgileManagement.Core;
using AgileManagement.Domain;
using AgileManagement.Domain.events;
using AgileManagement.Domain.handler;
using Microsoft.Extensions.DependencyInjection;
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

            foreach (var handler in _serviceProvider.GetServices(typeof(IDomainEventHandler<TEvent>)))
            {
                ((dynamic)handler).Handle(@event);
            }

       
        }
    }
}
