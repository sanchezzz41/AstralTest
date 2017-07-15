using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.ContextDb;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.Interface;
using AstralTest.DataDb;
using AstralTest.Domain.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем
    /// </summary>
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUser _context;
        public UserController(IUser context)
        {
            _context = context;
        } 

        //Возвращает всех пользователей
        [HttpGet("")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.GetAsync();
        }

        //Удаляет пользователя по Id
        [HttpDelete("{id}")]
        public async  Task<IActionResult> Delete(Guid id)
        {
            await _context.DeleteAsync(new User { Id = id });
            return RedirectToAction("GetUsers");
        }

        //Добавляет пользователя
        [HttpPost("")]
        public async Task<Guid> AddUser([FromBody] UserModel us)
        {
            var user = new User { Name = us.Name };
            var result = await _context.AddAsync(user);
            return result;
        }

        //Изменяет пользователя
        [HttpPut("")]
        public async Task<IActionResult> EditUser([FromBody] UserEditModel us)
        {
            var userForEdit = new User { Id = us.IdUser, Name = us.Name };
            await _context.EditAsync(userForEdit);
            return View(us.Name);
        }
    }

}
