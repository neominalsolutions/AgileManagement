
using AgileManagement.Core.validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    /// <summary>
    /// UserCreateRequestDto validate edebilecek IValidator tan implemente olan bir interface tanımladık
    /// </summary>
    public interface IUserRegisterValidator : IValidator<UserRegisterRequestDto>
    {
       
    }
}
