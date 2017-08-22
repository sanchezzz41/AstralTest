using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;
using AstralTest.Domain.Interfaces;
using AstralTest.Identity.JWTModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstrelTestWithToken.Controllers
{
    //Контроллер для авторизации пользователей
    [Authorize]
    [Route("Auth")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailSender _emailService;
        private readonly IJWTService _jwtService;

        public AccountController(IUserService userService, IEmailSender email, IJWTService jwtService)
        {
            _userService = userService;
            _emailService = email;
            _jwtService = jwtService;
        }


        //Post:Регистрация пользователя
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<object> Register([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.Select(x => new {Key = x.Key, Error = x.Value.Errors.Select(a => a.ErrorMessage)});
            }
            var resultId = await _userService.AddAsync(model);

            //Тут отправляем сообщение пользователю(либо для подверждения, либо ещё
            //для чего либо, но пока только в логи записываем это)
            await _emailService.SendEmail(model.Email, model.UserName, "Регистрация прошла успешна.");

            return "Вы успешно зарегестрировались.";
        }

        [HttpPost("Token")]
        [AllowAnonymous]
        public async Task<object> GetToken([FromBody] LoginViewModel model)
        {
            var result = await _jwtService.CreateToken(model.UserName, model.Password);
            return result;
        }
    }
}
