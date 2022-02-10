using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class UserCreatedNotifyHandler : IDomainEventHandler<UserCreatedEvent>
    {
        public void Handle(UserCreatedEvent @event)
        {
           
        }
    }
}
