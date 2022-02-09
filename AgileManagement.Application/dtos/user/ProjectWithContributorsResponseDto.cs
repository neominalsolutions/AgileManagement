using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application.dtos.user
{
    public class ContributorDto
    {

        public string UserId { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }


    }

    public class ProjectDto
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        /// <summary>
        /// Projeye eklenen contributorler
        /// </summary>
        public List<ContributorDto> Contributors { get; set; } = new List<ContributorDto>();
    }

    public class ProjectWithContributorsResponseDto
    {


       public  List<ProjectDto> Projects = new List<ProjectDto>();
       
    }
}
