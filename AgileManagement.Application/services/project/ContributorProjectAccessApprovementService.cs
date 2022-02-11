using AgileManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    /// <summary>
    /// Contributor Proje gönderilen isteği kabul yada reddetmesini sağlar
    /// </summary>
    public class ContributorProjectAccessApprovementService : IContributorProjectAccessApprovementService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public ContributorProjectAccessApprovementService(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public bool OnProcess(ContributorProjectAccessApprovementDto request)
        {
            // Accepted Rejected Contributor Status
            // proje ile birlikte project contributor doldururuz ki projenin contributorlarına müdehale edelim
            var project = _projectRepository.GetQuery()
                .Include(x => x.Contributers)
                .FirstOrDefault(x => x.Id == request.ProjectId);

            var user = _userRepository.Find(request.UserId);

            if (user != null && project != null)
            {
                // aynı projede aynı contributor olamaz
                var contributor = project.Contributers.FirstOrDefault(x => x.UserId == user.Id);

                if (request.Approve)
                {
                    contributor.ChangeProjectAccess(ContributorStatus.Accepted);
                    _projectRepository.Save();
                    return true;
                }
                else
                {
                    contributor.ChangeProjectAccess(ContributorStatus.Rejected);
                    _projectRepository.Save();
                    return false;
                }

            }

            throw new Exception("Böyle bir proje için bir contributor isteği bulunamadı");
        }
    }
}
