using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstrelTestWithToken.Extensions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstrelTestWithToken.Controllers.Admin
{
    /// <summary>
    /// Контроллер для просмотра логов
    /// </summary>
    [Route("Logs")]
    [Authorize(Roles =nameof(RolesOption.Admin))]
    public class LogsController : Controller
    {
        private readonly IActionService _actionService;
        public LogsController(IActionService actionService)
        {
            _actionService = actionService;
        }

        //Возвращает все логи 
        [HttpGet]
        public async Task<object> GetLogs()
        {
            var logs = await _actionService.GetAsync();
            return logs.Select(x => x.LogView());
        }
    }
}
