using AgileManagement.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        /// <summary>
        /// IUserCreateService use-case yani kullanıcı isteğidir.
        /// </summary>
        private readonly IUserRegisterService _userRegisterService;

        public AccountsController(IUserRegisterService userRegisterService)
        {
            _userRegisterService = userRegisterService;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] UserRegisterRequestDto model)
        {
            var response = _userRegisterService.OnProcess(model);

            return Ok(response);
        }
    }
}
