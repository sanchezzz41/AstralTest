using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем,
    /// Доступен только для админов
    /// </summary>
    [Route("AdminPanel")]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _context;

        public AdminController(IUserService context)
        {
            _context = context;
        }

        //Возвращает всех пользователей
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.GetAsync();
        }

        //Удаляет пользователя по Id
        [HttpDelete("{id}")]
        public async  Task Delete(string id)
        {
            await _context.DeleteAsync(id);
            
        }

        //Добавляет пользователя
        [HttpPost]
        public async Task<Guid> AddUser([FromBody] UserRegisterModel us)
        {
            var result = await _context.AddAsync(us);
            return result;
        }

        //Изменяет пользователя
        [HttpPut("{id}")]
        public async Task EditUser([FromBody] EditUserModel us, string id)
        {
             await _context.EditAsync(us,id);
        }
    }

}
