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
    public class UserCreateService : IUserCreateService
    {
        private IUserDomainService _userManager;
        private IUserCreateValidator _validator;

        public UserCreateService(IUserDomainService userManager, IUserCreateValidator validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public UserCreateResponseDto OnProcess(UserCreateRequestDto request)
        {
            var response = new UserCreateResponseDto();
            var validResult = _validator.IsValid(request);

       

            if (validResult)
            {
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
                response.Message = string.Join(",", _validator.Errors);
            }

  
            return response;
        }
    }
}
