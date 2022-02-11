using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    public class ContributorProjectAccessApprovementDto
    {
        public string ProjectId { get; set; }
        public string UserId { get; set; }

        public bool Approve { get; set; }


    }
}
