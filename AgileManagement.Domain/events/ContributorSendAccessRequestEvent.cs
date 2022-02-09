using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class ContributorSendAccessRequestEvent : IDomainEvent
    {
        public string Name { get; set; } = "ContributorSendRequest";
        public string ProjectName { get; private set; }
        public string ProjectId { get; private set; }
        public string UserId { get; private set; }



        public ContributorSendAccessRequestEvent(string projectName, string projectId, string userId)
        {
            UserId = userId;
            ProjectName = projectName;
            ProjectId = projectId;
        }
    }
}
