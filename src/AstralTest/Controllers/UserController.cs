using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Extensions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    //Контроллер для редактирования пользователя, самим собой
    [Route("Users")]
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
        public async Task<object> GetUsers()
        {
            var result = await _userService.GetAsync();
            return result.Select(x=>x.UserView()); 
        }
    }
}
