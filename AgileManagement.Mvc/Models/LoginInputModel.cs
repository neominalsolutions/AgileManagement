using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Models
{
    public class LoginInputModel
    {
        [EmailAddress(ErrorMessage ="E-Posta formatında olmalı")]
        [Required(ErrorMessage ="E-posta adresi boş geçilemez")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password boş geçilemez")]
        public string Password { get; set; }


        public bool RememberMe { get; set; }

    }
}
