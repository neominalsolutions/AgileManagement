using AgileManagement.Application;
using AgileManagement.Application.services;
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
        private readonly IAccountVerifyService _accountVerifyService;

        public AccountController(IUserRegisterService userRegisterService, IMapper mapper, IDataProtectionProvider dataProtectionProvider, IUserRepository userRepository, IAccountVerifyService accountVerifyService)
        {
            _userRegisterService = userRegisterService;
            _mapper = mapper;
            _dataProtector = dataProtectionProvider.CreateProtector(UserTokenNames.EmailVerification);
            _userRepository = userRepository;
            _accountVerifyService = accountVerifyService;
        }

        /// <summary>
        /// E-Posta hesabının onaylanmasını sağlıyor
        /// </summary>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        /// 
        /*
         * https://localhost:5001/account/confirm?verificationCode=CfDJ8IPdS1iyWX1DtUb2QtdSbGD03urFKBKId2pXH6Ll7kJAs--mqqpv7jcbuHX5BGs0iD4Xit0HGHL5-ePrWAk_vw3F-3M7pIF6ikSlwelgotND2KimTOJHIntAp8PSad64Eog-znlzIPZsu3UhnKeCJNAq45sklNwnU_Vo4sgqxKRk
         */

        /*
         * 
         * https://localhost:5001/account/confirm?verificationCode=CfDJ8IPdS1iyWX1DtUb2QtdSbGCnqLtg5tGpRMXkf8s3RFxHjMCfL-uGtBAh4CP7wFtEFS334zjQo8i7hnJd1B0Bq8Ak3ajK4-8kFZ-vJu10Np3n0QvI6tuS8yBaCggf6eJOQsHQpeGXbbF2S1HKcGxEKy59YFQHiQoE3ifPga8KvXsf
         */
        public IActionResult Confirm(string verificationCode)
        {
            var userId = _dataProtector.Unprotect(verificationCode);
            var result = _accountVerifyService.OnProcess(userId);

            if (result)
                return Redirect("/Account/Login");
            

            //var user = _userRepository.Find(userId);


            //if(user != null)
            //{
            //     user.SetVerifyEmail();
            //    _userRepository.Save();

            //    //hesap aktivasyonu sonrası logine' yönlendir
            //    return Redirect("/Account/Login");
            //}

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


        public IActionResult Login()
        {
            return View();
        }
    }
}
