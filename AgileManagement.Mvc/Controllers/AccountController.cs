using AgileManagement.Application;
using AgileManagement.Domain.repositories;
using AgileManagement.Mvc.Models;
using AgileManagement.Mvc.Profiles;
using AgileManagement.Persistence.EF;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRegisterService _userRegisterService;
        private readonly IMapper _mapper; // IMapper interface ile ilgili servis ile haberleşiriz.


        public AccountController(IUserRegisterService userRegisterService, IMapper mapper)
        {
            _userRegisterService = userRegisterService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterInputModel model)
        {


            // mapper kütüphansei
            //var dto = new UserRegisterRequestDto
            //{
            //    Email = model.Email,
            //    Password = model.Password
            //};


           // birebir properytyler aynı isimdeyse direk eşleme gerçekleşir.
           // eğer isim tutmaz ise o alan bış geçilir.
           // ne girecek ne çıkacak diye _mapper.Map methoduna belirtip dto dönüştürüyoruz.
           var dto = _mapper.Map<RegisterInputModel, UserRegisterRequestDto>(model);

            var response = _userRegisterService.OnProcess(dto);

            if (!response.Success)
            {
                var errors = response.Message.Split(',');

                for (int i = 0; i < errors.Length; i++)
                {
                    //   <div asp-validation-summary="All"></div> ile hata mesajları ekranda gösterilsin diye yaptık
                    ModelState.AddModelError($"{i}", errors[i]);
                }
               
            }

            ViewBag.Message = response.Message;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
