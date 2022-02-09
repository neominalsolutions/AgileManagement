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

        public void Dispatch<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            foreach (var handler in _kernel.GetAll<IDomainEventHandler<TDomainEvent>>())
            {
                handler.Handle(@event);
            }
        }

        
    }
}
