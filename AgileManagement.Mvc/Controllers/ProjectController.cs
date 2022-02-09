using AgileManagement.Core;
using AgileManagement.Domain;
using AgileManagement.Domain.repositories;
using AgileManagement.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Controllers
{

    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public ProjectController(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
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

        public IActionResult List()
        {
            var projects = _projectRepository.GetQuery().Include(x => x.Contributers).Select(a => new ProjectListViewModel
            {
                ProjectId = a.Id,
                Name = a.Name,
                Description = a.Description,
                //Contributors = _userRepository.GetQuery().Where(x => a.Contributers.Any(c => c.UserId == x.Id)).Select(x => x.Email).ToList()
            }).ToList();

            return View(projects);
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

                ViewBag.Message = "Proje oluşturuldu";

                return View();
            }

            // Proje oluşturma sayfası
            return View();
        }


        [HttpGet]
        public IActionResult AddContributorRequest(string projectId)
        {
            var project = _projectRepository.Find(projectId);

            var projectContributerUsersId = _projectRepository.GetQuery().Include(x => x.Contributers).Where(x=> x.Id == projectId).SelectMany(x => x.Contributers).Select(x => x.UserId).ToList();

            var users = _userRepository.GetQuery().Select(a=> new UserViewModel {
            UserId = a.Id,
            Email = a.Email
            }).ToList();

            var model = new ProjectAddContributorViewModel
            {
                Users = users,
                Name = project.Name,
                ProjectId = project.Id
            };

            return View(model);

        }



        [HttpPost]
        public JsonResult AddContributorRequest([FromBody] ContributorInputModel model)
        {
            var project = _projectRepository.Find(model.ProjectId);

            foreach (var userId in model.UsersId)
            {
                //project.AddContributor(new Contributor(userId));
            }

            _projectRepository.Save();


            return Json("OK");
           
        }

       
    }
}
