using AgileManagement.Core;
using AgileManagement.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Controllers
{
    [Authorize]
    public class AuthBaseController : Controller
    {
        private readonly AuthenticatedUser _authenticatedUser;
        // authbaseden kalıtım alan sınıflara authUser aktar
        protected AuthenticatedUserModel authUser;

        /// <summary>
        /// Authenticated olan kullanıcıların kullandığı bir controller base controller. Authenticated olan kullanıcı bilgisini tüm uygulama genelinde AuthenticatedUserModel tipinde döndürür.
        /// </summary>
        /// <param name="authenticatedUser"></param>
        public AuthBaseController(AuthenticatedUser authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
            authUser = _authenticatedUser.Info;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
