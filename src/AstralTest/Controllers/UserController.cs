using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем
    /// </summary>
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserService _context;
        public UserController(IUserService context)
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
        public async  Task Delete(Guid id)
        {
            await _context.DeleteAsync(id);
            
        }

        //Добавляет пользователя
        [HttpPost]
        public async Task<Guid> AddUser([FromBody] UserModel us)
        {
            var result = await _context.AddAsync(us);
            return result;
        }

        //Изменяет пользователя
        [HttpPut("{id}")]
        public async Task EditUser([FromBody] UserModel us,Guid id)
        {
             await _context.EditAsync(us,id);
        }
    }

}
