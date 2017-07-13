using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Context;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.Model;
using AstralTest.Domain.Interface;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IUser _context;
        public HomeController(IUser context)
        {
        
            _context = context;
            var result = _context.Users.FirstOrDefault();
        } 

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return View( _context.Users);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _context.DeleteUser(new User { Id = id });
            return RedirectToAction("GetUsers");
        }

        [HttpPost("Add")]
        public IActionResult AddUser([FromBody] User us)
        {
            _context.AddUser(us);
            return View(us.Name);
        }

        [HttpPut("Edit")]
        public IActionResult EditUser([FromBody] User us)
        {
            _context.EditUser(us);
            return View(us.Name);
        }
    }

}
