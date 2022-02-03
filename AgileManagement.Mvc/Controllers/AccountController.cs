using AgileManagement.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserCreateService _userCreateService;

        public AccountController(IUserCreateService userCreateService)
        {
            _userCreateService = userCreateService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserCreateRequestDto model)
        {
            var response = _userCreateService.OnProcess(model);
            ViewBag.Message = response.Message;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
