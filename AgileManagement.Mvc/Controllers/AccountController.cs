using AgileManagement.Application;
using AgileManagement.Core;
using AgileManagement.Domain;
using AgileManagement.Domain.conts;
using AgileManagement.Mvc.Models;
using AgileManagement.Mvc.Profiles;
using AgileManagement.Persistence.EF;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgileManagement.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRegisterService _userRegisterService;
        private readonly IMapper _mapper; // IMapper interface ile ilgili servis ile haberleşiriz.
        private readonly IDataProtector _dataProtector;
        private readonly IAccountVerifyService _accountVerifyService;
        private readonly IUserLoginService _userLoginService;


        public AccountController(IUserRegisterService userRegisterService, IMapper mapper, IDataProtectionProvider dataProtectionProvider, IAccountVerifyService accountVerifyService,IUserLoginService userLoginService)
        {
            _userRegisterService = userRegisterService;
            _mapper = mapper;
            _dataProtector = dataProtectionProvider.CreateProtector(UserTokenNames.EmailVerification);

            _accountVerifyService = accountVerifyService;
            _userLoginService = userLoginService;
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
           //string email =   User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

            return View();
        }

        [HttpPost][AllowAnonymous] // herkes istek atabilir.
        public IActionResult Login([FromForm] LoginInputModel model)
        {
            if(ModelState.IsValid)
            {
                var dto = _mapper.Map<LoginInputModel, UserLoginRequestDto>(model);

                var result = _userLoginService.OnProcess(dto);

                if (result.IsSucceeded)
                {
                 
                    return Redirect(result.ReturnUrl);
                }
                
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return View();
                }
            }

            return View();
            

            //var user = _userRepository.FindUserByEmail(model.Email);

            //if (ModelState.IsValid)
            //{
            //    if (user == null)
            //    {
            //        ViewBag.Message = "Kullanıcı hesabı bulunamadı";
            //        return View();
            //    }
            //    else
            //    {
            //        var hashedPassword = _passwordHasher.HashPassword(model.Password);

            //        if (user.PasswordHash != hashedPassword)
            //        {
            //            ViewBag.Message = "Parola hatalı";
            //            return View();
            //        }

            //        if (!user.EmailVerified)
            //        {
            //            ViewBag.Message = "Hesap aktif değil!";
            //            return View();
            //        }

            //        var claims = new List<Claim>
            //        {
            //            new Claim(ClaimTypes.Name, user.UserName),
            //            new Claim(ClaimTypes.GivenName,$"{user.FirstName} {user.MiddleName}{user.LastName}"),
            //            new Claim(ClaimTypes.Email, user.Email)
            //        };


            //        var principle = new ClaimsPrincipal();

            //        var identity = new ClaimsIdentity(claims, "NormalScheme");
            //        principle.AddIdentity(identity);


            //        var properties = new AuthenticationProperties();

            //        properties.ExpiresUtc = DateTime.UtcNow.AddDays(30);
            //        properties.IsPersistent = model.RememberMe; // cookie kalıcı mı olsun session bazlı tarayıcı kapatınca cookie silinsin mi değeri

            //       HttpContext.SignInAsync("NormalScheme", principle, properties).GetAwaiter().GetResult();
            //        // burada cookie değeri oluşuyor.



            //        // awaitsiz olarak asenkron kod çalıştırma şekli

            // return Redirect("/"); // anasayfaya döndür.
        //  } }
            //
               

        }

        // Authenticated olan kullanıcı bu sayfaya erişebilir. Bu methodu tetişkleyebilir.
        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync("NormalScheme");
            return Redirect("/");
        }
    }
}
