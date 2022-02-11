
using AgileManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    public class ProjectWithContributorsRequestService : IProjectWithContributorsRequestService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectWithContributorsRequestService(IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        public ProjectWithContributorsResponseDto OnProcess(ProjectWithContributorRequestDto request =  null)
        {


            var query = _projectRepository.GetQuery();

            if (!string.IsNullOrEmpty(request.CreatedBy))
            {
               query =  query.Where(x => x.CreatedBy == request.CreatedBy);
            }

            if (request != null && !string.IsNullOrEmpty(request.ProjectId))
            {
               query =  query.Where(x => x.Id == request.ProjectId);
            }

            var projects = query.Include(x => x.Contributers).Select(a => new ProjectDto
            {
                ProjectId = a.Id,
                Name = a.Name,
                Description = a.Description,
                Contributors = a.Contributers.Select(x => new ContributorDto
                {
                    ProjectName = x.Project.Name,
                    UserId = x.UserId
                }).ToList()

            }).ToList();


            projects.ForEach(project =>
            {
                List<string> contributersUserId = project.Contributors.Select(x => x.UserId).ToList();

                project.Contributors = _userRepository.GetQuery().Where(x => contributersUserId.Contains(x.Id)).Select(y => new ContributorDto
                {
                    Email = y.Email,
                    UserName = y.UserName,
                    UserId = y.Id,
                    ProjectName = project.Name

                }).ToList();
            });

            var response = new ProjectWithContributorsResponseDto
            {
                Projects = projects
            };

            return response;
        }
    }
}
