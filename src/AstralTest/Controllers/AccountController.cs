using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;
using AstralTest.Domain.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    //Контроллер для авторизации пользователей
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signManager;
        //Интерфейс для работы с пользователями
        private readonly IUserService _userService;
        private IEmailSender _emailService;
        public AccountController(SignInManager<User> sign,IUserService userService,IEmailSender email)
        {
            _signManager = sign;
            _userService = userService;
            _emailService = email;
        }

        //Get:Возвращает окно регистрации
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        //Post:Регистрация пользователя
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromForm]UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var resultId = await _userService.AddAsync(model);
            if (resultId != Guid.Empty)
            {
                //Тут отправляем сообщение пользователю(либо для подверждения, либо ещё
                //для чего либо, но пока только в логи записываем это)
                await _emailService.SendEmail(model.Email, model.UserName, "Регистрация прошла успешна");
                //Ищем добавленного пользователя, и авторезируем его
                var newList =await _userService.GetAsync();
                var newUser = newList.Single(x => x.Id == resultId.ToString());

                await _signManager.SignInAsync(newUser, false);
                return LocalRedirect("/swagger");
            }
            return View(model);
        }

        //Get: Возвращает окно для входа
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string ReturnUrl = "")
        {
            return View(new LoginViewModel { ReturnUrl = ReturnUrl });
        }

        //Post: Вход в приложение
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromForm]LoginViewModel model)
        {
            var res = await _signManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (res.Succeeded)
            {
                return LocalRedirect("/swagger");
            }
            return View(model);
        }

        //Выход из приложения
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
