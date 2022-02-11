using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Areas.Admin.Models
{
    public class ContributorInputModel
    {
        public List<string> UsersId { get; set; } = new List<string>();
        public string ProjectId { get; set; }

    }
}
