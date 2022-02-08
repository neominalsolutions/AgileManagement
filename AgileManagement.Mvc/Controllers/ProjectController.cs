using AgileManagement.Core;
using AgileManagement.Domain;
using AgileManagement.Domain.repositories;
using AgileManagement.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Controllers
{

    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public ProjectController(IProjectRepository projectRepository, IDomainEventDispatcher domainEventDispatcher)
        {
            _projectRepository = projectRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// İlgili Projede ilgili contributor'a erim izni verir.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="accepted"></param>
        /// <returns></returns>
        public IActionResult AcceptRequest(string projectId, string userId, bool accepted)
        {
            // Accepted Rejected Contributor Status
            return View();
        }

        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(ProjectCreateInputModel projectCreateInputModel)
        {
            if (ModelState.IsValid)
            {
                var project = new Project(name: projectCreateInputModel.Name, description: projectCreateInputModel.Description);

                _projectRepository.Add(project);
                _projectRepository.Save();

                return View();
            }

            // Proje oluşturma sayfası
            return View();
        }

        [HttpPost]
        public JsonResult AddContributorRequest([FromBody] ContributorInputModel model)
        {
            var project = _projectRepository.Find(model.ProjectId);

            foreach (var userId in model.UsersId)
            {
                project.AddContributor(new Contributor(userId), _domainEventDispatcher);
            }

            _projectRepository.Save();


            return Json("OK");
           
        }

       
    }
}
