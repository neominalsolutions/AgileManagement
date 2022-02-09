using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Models
{
    public class ProjectListViewModel
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<string> Contributors { get; set; } = new List<string>();

    }
}
