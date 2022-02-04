using AgileManagement.Application;
using AgileManagement.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRegisterService _userRegisterService;

        public AccountController(IUserRegisterService userRegisterService)
        {
            _userRegisterService = userRegisterService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterInputModel model)
        {
            var dto = new UserRegisterRequestDto
            {
                Email = model.Email,
                Password = model.Password
            };

            var response = _userRegisterService.OnProcess(dto);
            ViewBag.Message = response.Message;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
