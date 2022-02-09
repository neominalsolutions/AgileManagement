using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    /// <summary>
    /// Bu interface event işleyici olarak çalışır.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent:IDomainEvent
    {
        void Handle(TDomainEvent @event);
    }
}
