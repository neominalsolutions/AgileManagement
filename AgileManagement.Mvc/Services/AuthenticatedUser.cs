using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Services
{
    public class AuthenticatedUserModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }

        public string GivenName { get; set; }
    }

    public class AuthenticatedUser
    {
        // Eğer bir servisin içerisindeysek HttpContext üzerinden değer okumak için IHttpContextAccessor kullanırız

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
 
        public  AuthenticatedUserModel Info
        {
            get
            {
                return new AuthenticatedUserModel
                {
                    Email = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value,
                    UserId = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "UserId").Value,
                    UserName = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value,
                    GivenName = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.GivenName).Value

                };
            }
        }
    }
}
