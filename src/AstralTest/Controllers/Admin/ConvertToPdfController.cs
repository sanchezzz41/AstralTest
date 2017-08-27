using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Logs;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Web.Controllers.Admin
{
    [Route("ConverterToPdf")]
    [Authorize(Roles = nameof(RolesOption.Admin))]
    public class ConvertToPdfController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogService<LogModel> _logService;
        private readonly IActionService _actionService;

        public ConvertToPdfController(IUserService userService,IActionService actionSerivce,ILogService<LogModel> logService)
        {
            _userService = userService;
            _logService = logService;
        }

        [HttpGet("Users")]
        public async Task<object> GetUsers()
        {
            var list = await _userService.GetAsync();
            var resultBytes = await _userService.ConvertToPdf(list);
            if (resultBytes != null)
            {
                return File(resultBytes, "application/pdf","Users.pdf");
            }
            Response.StatusCode = 501;
            return "Внутренная ошибка";
        }

        [HttpGet("Logs")]
        public async Task<object> GetLogs()
        {
            var list = await _actionService.GetAsync();
            var resultList = new List<LogModel>();
            foreach (var item in list)
            {
              if(item.InfoAboutActions!=null)
                {
                    resultList.Add(new LogModel { UserName=item.User.UserName,NameController=item.NameOfController,NameAction=item.NameOfAction,Parametrs=item.InfoAboutActions.pa})
                }
            }
            var resultBytes = await _userService.ConvertToPdf(list);
            if (resultBytes != null)
            {
                return File(resultBytes, "application/pdf", "Users.pdf");
            }
            Response.StatusCode = 501;
            return "Внутренная ошибка";
        }
    }
}
