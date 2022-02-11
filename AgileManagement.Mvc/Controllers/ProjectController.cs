using AgileManagement.Application;
using AgileManagement.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Controllers
{
    public class ProjectController : AuthBaseController
    {
        private readonly IProjectWithContributorsRequestService _projectWithContributorsRequestService;
        private readonly IContributorProjectAccessApprovementService _contributorProjectAccessApprovementService;

    

        public ProjectController(AuthenticatedUser authenticatedUser, IProjectWithContributorsRequestService projectWithContributorsRequestService, IContributorProjectAccessApprovementService contributorProjectAccessApprovementService) : base(authenticatedUser)
        {
            _projectWithContributorsRequestService = projectWithContributorsRequestService;
            _contributorProjectAccessApprovementService = contributorProjectAccessApprovementService;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult ApprovedProject(string projectId)
        {
            // burada bu projenin contributor'ı olup olmadığını kontrol ederiz.
            // ona göre proje detaylarını göreceğimiz sayfa gelir.

            var request = new ProjectWithContributorRequestDto
            {
                ProjectId = projectId
            };


            var response = _projectWithContributorsRequestService.OnProcess(request);


            if (response.Projects.Count() > 0)
            {
                if (!response.Projects[0].Contributors.Any(x => x.UserId == authUser.UserId))
                {
                    return Unauthorized();
                }
                else
                {

                    return View(response.Projects[0]);
                }
            }

            return View();
        }

        /// <summary>
        /// İlgili Projede ilgili contributor'a erim izni verir.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="accepted"></param>
        /// <returns></returns>
        public IActionResult ContributorProjectApprove(string projectId, string userId, bool accepted)
        {
            var request = new ContributorProjectAccessApprovementDto
            {
                ProjectId = projectId,
                UserId = userId,
                Approve = accepted
            };

            var approved = _contributorProjectAccessApprovementService.OnProcess(request);

            ViewBag.Message = approved ? "Projeye erişimizi var" : "Proje erimi red edildi";
            ViewBag.Status = approved;
            ViewBag.ProjectId = projectId;


            return View();
        }
    }
}
