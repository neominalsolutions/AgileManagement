using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    public class UserLoginResponseDto
    {
        public string ErrorMessage { get; set; } // işlem başarılı değilse döneceğimiz değer 
        public string ReturnUrl { get; set; } // Login olduktan sonra gidilecek olan Url bilgisi
        public bool IsSucceeded { get; set; } // giriş işleminin başarılı olup olmadığı bilgisi


    }
}
