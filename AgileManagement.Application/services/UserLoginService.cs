
using AgileManagement.Core;
using AgileManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    public class UserLoginService : IUserLoginService
    {
        private readonly ICookieAuthenticationService _cookieAuthenticationService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserLoginService(ICookieAuthenticationService cookieAuthenticationService, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _cookieAuthenticationService = cookieAuthenticationService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public UserLoginResponseDto OnProcess(UserLoginRequestDto request)
        {


            var user = _userRepository.FindUserByEmail(request.Email);
            var response = new UserLoginResponseDto();

            // user role ataması var mı yok mu kontrolü yapılarak, rolü varsa scheme SecureScheme olarak atanır
            // yoksa NormalScheme olarak burada atanabilir. Bizde rol mantığı şuan için yok.


            if (user == null)
            {
                response.ErrorMessage = "Kullanıcı hesabı bulunamadı";
                response.IsSucceeded = false;

            }
            else
            {
                var hashedPassword = _passwordHasher.HashPassword(request.Password);

                if (user.PasswordHash != hashedPassword)
                {
                    response.ErrorMessage = "Parola hatalı";
                    response.IsSucceeded = false;

                }

                if (!user.EmailVerified)
                {
                    response.ErrorMessage = "Hesap aktif değil!";
                    response.IsSucceeded = false;

                }

                response.ReturnUrl = "/";
                _cookieAuthenticationService.AuthenticateAsync(request.Email, request.Password, request.RememberMe, "NormalScheme").GetAwaiter().GetResult();

                // eğer async bir method askenron olmayan bir method içerisinde çağırılacak ise bu durumda await yazamayacağımız için asenkron method sonuna GetAwaiter().GetResult(); methodunu koyarak senkronlaştırabiliriz.
                response.IsSucceeded = true;

            }

            return response;
            

        }
    }
}
