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
    /// <summary>
    /// Контроллер для конвертации данных в пдф
    /// </summary>
    [Route("ConverterToPdf")]
    [Authorize(Roles = nameof(RolesOption.Admin))]
    public class ConvertToPdfController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogService<LogModel> _logService;
        private readonly IActionService _actionService;

        public ConvertToPdfController(IUserService userService, IActionService actionSerivce, ILogService<LogModel> logService)
        {
            _userService = userService;
            _logService = logService;
            _actionService = actionSerivce;
        }

        //Возвращает Pdf с пользователями
        [HttpGet("Users")]
        public async Task<object> GetUsers()
        {
            var list = await _userService.GetAsync();
            var resultBytes = await _userService.ConvertToPdf(list);
            if (resultBytes != null)
            {
                return File(resultBytes, "application/pdf", "Users.pdf");
            }
            Response.StatusCode = 501;
            return "Внутренная ошибка";
        }

        //Возвращает Pdf с логами
        [HttpGet("Logs")]
        public async Task<object> GetLogs()
        {
            var list = await _actionService.GetAsync();
            var resultList = new List<LogModel>();
            foreach (var item in list)
            {
                var resultModel = new LogModel
                {
                    UserName = item.User.UserName,
                    NameController = item.NameOfController,
                    NameAction = item.NameOfAction,
                    Parametrs = ""
                };
                if (item.ParametrsAction != null)
                {
                    resultModel.Parametrs = item.ParametrsAction.JsonParametrs;
                }
                resultList.Add(resultModel);
            }
            var resultBytes = await _logService.ConvertToPdf(resultList);

            if (resultBytes != null)
            {
                return File(resultBytes, "application/pdf", "Logs.pdf");
            }

            Response.StatusCode = 501;
            return "Внутренния ошибка";
        }
    }
}
