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
        private readonly IUserCreateService _userCreateService;

        public AccountsController(IUserCreateService userCreateService)
        {
            _userCreateService = userCreateService;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] UserCreateRequestDto model)
        {
            var response = _userCreateService.OnProcess(model);

            return Ok(response);
        }
    }
}
