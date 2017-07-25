using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Extensions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем,
    /// Доступен только для админов
    /// </summary>
    [Route("Admin")]
    [Authorize(Roles = nameof(RolesOption.Admin))]
    public class UsersController : Controller
    {
        private readonly IUserService _context;

        public UsersController(IUserService context)
        {
            _context = context;
        }

        //Возвращает всех пользователей
        [HttpGet("Users")]
        public async Task<object> GetUsers()
        {
            var result = await _context.GetAsync();
            return result.UsersForAdminView(HttpContext);
        }

        //Удаляет пользователя по Id
        [HttpDelete("Users/{id}")]
        public async Task DeleteUser(Guid id)
        {
            if(HttpContext.User.IsInRole(RolesOption.Admin.ToString()))
            {
                var resultUser = await _context.GetAsync();
            }
            await _context.DeleteAsync(id);

        }

        //Добавляет пользователя, скорей всего не нужно(как админ может кого то добавить, не спрашиваю его)
        [HttpPost("AddUser")]
        public async Task<Guid> AddUser([FromBody] UserRegisterModel us)
        {
            var result = await _context.AddAsync(us);
            return result;
        }

        //Изменяет пользователя
        [HttpPut("EditUser/{id}")]
        public async Task EditUser([FromBody] EditUserModel us, Guid id)
        {
            await _context.EditAsync(us, id);
        }
    }
}
