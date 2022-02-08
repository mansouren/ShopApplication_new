using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ShopApplication.Common.Utilities;
using ShopApplication.Common.Utilities.Common.Utilities;

using ShopApplication.DataLayer.Repositories;
using ShopApplication.Models;
using ShopApplication.Services;
using ShopApplication.Services.Contracts;
using ShopApplication.WebFrameWorks.Scope;
using System;

using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;


namespace ShopApplication.Controllers
{
    [AllowAnonymous]
    
    public class AccountController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly IJwtService jwtService;
        private readonly IHttpClientFactory clientFactory;

        public AccountController(IMapper mapper, IUserRepository userRepository,
            IUserService userService ,IJwtService jwtService,IHttpClientFactory clientFactory)

        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.userService = userService;
            this.jwtService = jwtService;
            this.clientFactory = clientFactory;
            
        }
        public IActionResult Index()
        {
            return View();
        }


        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if (await userService.IsExistMobile(registerDto.Mobile))
            {
                ModelState.AddModelError("Mobile", "شماره موبایل وارد شده تکراری است");
                return View(registerDto);
            }

            if (ModelState.IsValid)
            {
                var model = registerDto.ToEntity(mapper);
                string text = await userRepository.AddAsync(model, cancellationToken);
                SendSms sms = new SendSms(clientFactory);
                await sms.SendMessagess(model.Mobile, text);
                return RedirectToAction("Login");
            }

            return View();

        }

        

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.IsExistUser(loginDto.Mobile, loginDto.Password);

                if (user != null)
                {

                    var response = jwtService.GenerateToken(user);
                    HttpContext.Session.SetString("JWToken", response.AccessToken);
                    await userService.UpdateRefreshToken(response.RefreshToken, user, cancellationToken);
                    SetRefreshTokenInCookie(response.RefreshToken);
                    SiteLayoutScope.IsAuthenticated = true;
                    if (user.IsActive)
                    {
                        if (user.Role.RoleName == "Admin")
                         
                            return Redirect($"/Admin");
                        else
                            return RedirectToAction("Index", "Profile");
                        
                    }

                    return RedirectToAction("Activate");

                }
                else
                {
                    ModelState.AddModelError("Password", "کاربر نامعتبر است!");
                    return View(loginDto);
                }
            }


            return View();
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookiOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshtoken", refreshToken, cookiOptions);
        }

        [Route("Activate")]
        public IActionResult Activate()
        {
            return View();
        }

        [HttpPost("Activate")]
        public async Task<IActionResult> Activate(ActivateDto activateDto, CancellationToken cancellationToken)
        {
            string mobile = User.Identity.FindFirstValue(ClaimTypes.Name);
            var user = await userService.GetUserByUserCodeAndMobile(activateDto.UserCode, mobile);
            if (user != null)
            {
                Random random = new Random();
                var usercode = random.Next(100000, 900000);
                user.IsActive = true;
                user.UserCode = usercode.ToString();

                await userRepository.UpdateAsync(user, cancellationToken);
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                ModelState.AddModelError("UserCode", "کد فعال سازی معتبر نمی باشد");
            }

            return View(activateDto);
        }

        [HttpGet("CheckMobile")]
        public IActionResult CheckMobile()
        {
            return View();
        }

        [HttpPost("CheckMobile")]
        public async Task<IActionResult> CheckMobile(CheckMobileDto check)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.GetUserByMobile(check.Mobile);
                if (user != null)
                {
                    SendSms sendSms = new SendSms(clientFactory);
                    await sendSms.SendMessagess(check.Mobile, "کد تائید شما برای تغییر کلمه عبور : " + user.UserCode + "می باشد.");
                    return RedirectToAction("ForgetPassword");
                }
                ModelState.AddModelError("Mobile", "شما هنوز ثبت نام نکرده اید");
            }

            return View(check);

        }

        [HttpGet("ForgetPassword")]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordDto forgetPassword, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.GetUserByUserCode(forgetPassword.UserCode);
                if (user != null)
                {
                    await userService.ForgetPassword(forgetPassword.Password, user, cancellationToken);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("UserCode", "کد تایید وارد شده معتبر نمی باشد.");
                }
            }
            return View(forgetPassword);
        }

        public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByMobile(User.Identity.Name);
            HttpContext.Session.Clear();
            Response.Cookies.Delete("refreshtoken");
            await userService.UpdateRefreshToken(string.Empty, user, cancellationToken);
            SiteLayoutScope.IsAuthenticated = false;
            return RedirectToAction("Index", "Home");
            
        }

       
    }
}
