using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    /// <summary>
    /// Hesap açılışı için gerekli olan alanlar
    /// </summary>
    public class UserRegisterRequestDto
    {

        public string Email { get; set; }
        public string Password { get; set; }

        public string PhoneNumber { get; set; }


    }
}
