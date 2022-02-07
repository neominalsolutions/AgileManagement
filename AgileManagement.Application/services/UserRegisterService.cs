using AgileManagement.Application;
using AgileManagement.Core.validation;
using AgileManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    public class UserRegisterService : IUserRegisterService
    {
        private IUserDomainService _userManager;
        private IUserRegisterValidator _validator;

        public UserRegisterService(IUserDomainService userManager, IUserRegisterValidator validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public UserRegisterResponseDto OnProcess(UserRegisterRequestDto request)
        {
            var response = new UserRegisterResponseDto();
            var validResult = _validator.IsValid(request);
       

            if (validResult)
            {
                response.Success = true;

                var result = _userManager.CreateUser(request.Email, request.Password);

                if (result.IsSucceeded)
                {
                    response.Message = "Kullanıcı kaydı başarılıdır. E-Posta adresinize hesabı onaylamak için bir link gönderdik.";
                }
                else
                {
                    response.Message = string.Join(",", result.Errors);
                }
            }
            else
            {
                response.Success = false;
                response.Message = string.Join(",", _validator.Errors);
            }

  
            return response;
        }
    }
}
