using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    public class UserRegisterValidator : IUserRegisterValidator
    {

        public List<string> Errors { get; set; } = new List<string>();

        public bool IsValid(UserRegisterRequestDto @object)
        {
           

            if (string.IsNullOrEmpty(@object.Email))
            {
                Errors.Add("E-posta alanı boş geçilemez");
            }
            else
            {
                if (!@object.Email.Contains("@"))
                {
                    Errors.Add("E-posta formatına uygun değildir");
                }
            }
           

            if (string.IsNullOrEmpty(@object.Password))
            {
                Errors.Add("Parola boş geçilemez");
            }
            else
            {
                if (@object.Password.Length < 8)
                {
                    Errors.Add("Parola minimum 8 karakter olmalıdır");
                }

                // @" ^ (?=.*?[A - Z])(?=.*?[a - z])(?=.*?[0 - 9])(?=.*?[#?!@$%^&*-]).{8,}$"

                string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";

                if (!Regex.IsMatch(@object.Password, pattern))
                {
                    Errors.Add("Daha kompleks bir parola giriniz");
                }
            }

         

           return  Errors.Count() > 0 ? false : true;



        }
    }
}
