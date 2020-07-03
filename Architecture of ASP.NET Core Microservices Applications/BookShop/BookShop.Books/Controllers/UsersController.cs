namespace BookShop.Books.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Books.Models.Users;
    using BookShop.Books.Services.Models.Users;
    using BookShop.Books.Services.Users;
    using BookShop.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : AdministrationController
    {
        private readonly IUserService userService;

        private readonly IMapper mapper;

        public UsersController(IUserService userService,  IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return await this.Handle(
                async () =>
                {
                    var result = await this.userService
                        .Register(this.mapper.Map<UserRegisterServiceModel>(model));

                    this.Response.Cookies.Append(
                        InfrastructureConstants.AuthenticationCookieName,
                        result.Token,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            MaxAge = TimeSpan.FromDays(1)
                        });
                },
                success: RedirectToAction(nameof(HomeController.Index), "Home"),
                failure: RedirectToAction(nameof(HomeController.Index), "Home"));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();        

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return await this.Handle(
                async () =>
                {
                    var result = await this.userService
                        .Login(this.mapper.Map<UserServiceModel>(model));

                    this.Response.Cookies.Append(
                        InfrastructureConstants.AuthenticationCookieName,
                        result.Token,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            MaxAge = TimeSpan.FromDays(1)
                        });
                },
                success: RedirectToAction(nameof(HomeController.Index), "Home"),
                failure: RedirectToAction(nameof(HomeController.Index), "Home"));
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(InfrastructureConstants.AuthenticationCookieName);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}