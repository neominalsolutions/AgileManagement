using AgileManagement.Core;
using AgileManagement.Domain;
using AgileManagement.Domain.events;
using AgileManagement.Domain.handler;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Infrastructure.events
{
    public static class NinjectEventModule
    {
        public static void RegisterServices(IKernel kernel)
        {
            

            kernel.Bind<IDomainEventHandler<UserCreatedEvent>>().To<UserCreatedHandler>();
            kernel.Bind<IDomainEventHandler<ContributorSendAccessRequestEvent>>().To<ContributerSendAccessRequestHandler>();
            kernel.Bind<IDomainEventHandler<ContributorRevokeAccessEvent>>().To<ContributorRevokeAccessEventHandler>();

            DomainEvent._domainEventDispatcher = new NinjectDomainEventDispatcher(kernel);

        }
    }
}
