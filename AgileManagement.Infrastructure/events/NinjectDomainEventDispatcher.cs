using AgileManagement.Core;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Infrastructure.events
{
    public class NinjectDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IKernel _kernel;

        public NinjectDomainEventDispatcher(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            foreach (var handler in _kernel.GetAll<IDomainEventHandler<TEvent>>())
            {
                handler.Handle(@event);
            }
        }
    }
}
