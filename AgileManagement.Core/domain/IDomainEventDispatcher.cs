using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    /// <summary>
    /// Bu sınıf bir event raise olduktan sonra eventin ilgili handler'a sevk edilmesinden sorumlu olan ara bir yapı görevi görür. Böylece event üzerinden taşınan bilgileri bağımsız bir şekilde sisteme tanımlanmış olan Handler'a iletmekten sorumludur.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IDomainEventDispatcher<TEvent> where TEvent:IDomainEvent
    {
        void Dispatch(TEvent @event);
    }
}
