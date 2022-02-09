using AgileManagement.Application;
using AgileManagement.Mvc.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            // mvc nesnesini application nesnesine mapleyecek.
            // post işlemlerinde mvc nesnesi application nesnesine mapplenir.
            // eşlemek.
            CreateMap<RegisterInputModel, UserRegisterRequestDto>();
            // get işlemlerinde ise application dto viewmodel yani mvc nesnesine dönüşür.
            // profile consturcture içerisine user ile ilgili istedeiğimiz kadar create map açabiliriz.

            CreateMap<LoginInputModel, UserLoginRequestDto>();
        }
        
    }
}
