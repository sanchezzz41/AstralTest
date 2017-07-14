using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.ContextDb;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.Interface;
using AstralTest.DataDb;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUser _context;
        public UserController(IUser context)
        {
        
            _context = context;
            var result = _context.Users.FirstOrDefault();
        } 

        [HttpGet("")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return  _context.Users;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _context.DeleteUser(new User { Id = id });
            return RedirectToAction("GetUsers");
        }

        [HttpPost("")]
        public IActionResult AddUser([FromBody] User us)
        {
            _context.AddUser(us);
            return View(us.Name);
        }

        [HttpPut("")]
        public IActionResult EditUser([FromBody] User us)
        {
            _context.EditUser(us);
            return View(us.Name);
        }
    }

}
