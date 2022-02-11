using AgileManagement.Core;
using AgileManagement.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    /// <summary>
    /// Cookie ile sisteme kimlik doğrulama yapacağımız servis.
    /// </summary>
    public class CookieAuthenticationService : ICookieAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Eğer bir class içerisinde HttpContext bağlanmamız gerekirse IHttpContextAccessor interface kullanmalıyız.
        // SignInAsync işleminin application katmanından tetiklemek için ise Microsoft.AspNetCore.Authentication paketini bu katmana ekledik.

        public CookieAuthenticationService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task AuthenticateAsync(string email, string password, bool persistance, string scheme)
        {
            var user = _userRepository.FindUserByEmail(email);

            try
            {
                var claims = new List<Claim>
                    {
                        new Claim("UserId",user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.GivenName,$"{user.FirstName} {user.MiddleName}{user.LastName}"),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                var principle = new ClaimsPrincipal();

                var identity = new ClaimsIdentity(claims, scheme);
                principle.AddIdentity(identity);


                var properties = new AuthenticationProperties();

                properties.ExpiresUtc = DateTime.UtcNow.AddDays(30);
                properties.IsPersistent = persistance; // cookie kalıcı mı olsun session bazlı tarayıcı kapatınca cookie silinsin mi değeri

                await _httpContextAccessor.HttpContext.SignInAsync(scheme, principle, properties);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
