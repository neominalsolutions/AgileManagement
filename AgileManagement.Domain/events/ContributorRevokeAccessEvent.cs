using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class ContributorRevokeAccessEvent: IDomainEvent
    {
        public string ProjectName { get; private set; }
        public string UserId { get; private set; }

        public string Name { get; set; } = "ContributorRevokeAccessEvent";

        public ContributorRevokeAccessEvent(string projectName, string userId)
        {
            ProjectName = projectName;
            UserId = userId;
        }
    }
}
