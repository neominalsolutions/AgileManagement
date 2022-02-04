using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application.validators
{
    public class UserRegisterValidator : IUserRegisterValidator
    {
        public List<string> Errors { get; set; }

        public bool IsValid(UserRegisterRequestDto @object)
        {
            return true;
        }
    }
}
