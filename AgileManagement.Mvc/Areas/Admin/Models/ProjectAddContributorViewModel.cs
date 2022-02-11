using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string UserId { get; set; }

    }

    public class ProjectAddContributorViewModel
    {
        public string ProjectId { get; set; }

        public string Name { get; set; }

        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    }
}
