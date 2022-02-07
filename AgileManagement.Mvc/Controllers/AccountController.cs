using AgileManagement.Application;
using AgileManagement.Domain.conts;
using AgileManagement.Domain.repositories;
using AgileManagement.Mvc.Models;
using AgileManagement.Mvc.Profiles;
using AgileManagement.Persistence.EF;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
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
        private readonly IDataProtector _dataProtector;
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRegisterService userRegisterService, IMapper mapper, IDataProtectionProvider dataProtectionProvider, IUserRepository userRepository)
        {
            _userRegisterService = userRegisterService;
            _mapper = mapper;
            _dataProtector = dataProtectionProvider.CreateProtector(UserTokenNames.EmailVerification);
            _userRepository = userRepository;
        }

        /// <summary>
        /// E-Posta hesabının onaylanmasını sağlıyor
        /// </summary>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        public IActionResult Confirm(string verificationCode)
        {
            var userId = _dataProtector.Unprotect(verificationCode);
            var user = _userRepository.Find(userId);

            
            if(user != null)
            {
                 user.SetVerifyEmail();
                _userRepository.Save();

                //hesap aktivasyonu sonrası logine' yönlendir
                return Redirect("/Account/Login");
            }

            ViewBag.Message = "Hesabınız aktive edilemedi!";
            return View();
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
