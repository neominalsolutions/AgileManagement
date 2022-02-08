using AgileManagement.Core;
using AgileManagement.Domain.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain.handler
{
    public class ContributorRevokeAccessEventHandler : IDomainEventHandler<ContributorRevokeAccessEvent>
    {
        public void Handle(ContributorRevokeAccessEvent @event)
        {
          
        }
    }
}
