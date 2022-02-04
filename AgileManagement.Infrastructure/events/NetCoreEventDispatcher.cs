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
        private readonly IServiceProvider _serviceProvider;

        public NetCoreEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {

            var handler = _serviceProvider.GetService(typeof(IDomainEventHandler<TEvent>));
            ((dynamic)handler).Handle(@event);
        }
    }
}
