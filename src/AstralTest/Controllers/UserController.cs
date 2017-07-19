using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Entities;
using AstralTest.Database;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    //Контроллер для редактирования пользователя, самим собой
    [Route("User")]
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Возвращает список пользователей, в специальной обертке
        // GET: /<controller>/
        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var users = await _userService.GetAsync();
            if (users.Count == 0)
            {
                return null;
            }
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var item in users)
            {
                result.Add(new UserViewModel { Email = item.Email, UserName = item.UserName,
                    Id = item.Id, Notes = item.Notes });
            }
            //Почему то возвращает только 1 элемент списка
            return result;
        }

        //Изменяет пароль пользователя
        [HttpPost]
        public async Task<string> EditPassword([FromBody]EditPasswordModel editModel)
        {
            if(!ModelState.IsValid)
            {
                return "Заполните корректно данные";
            }
            var nameUser = HttpContext.User.Identity.Name;
           int result=await _userService.EditPasswordAsync(nameUser,editModel);
            if(result==1)
            {
                return "Пароль успешно обновлён.";
            }
            return "Не удалось сменить пароль.";
        }
    }
}
