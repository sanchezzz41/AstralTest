using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Tests.Domain.Entities.Factory
{
    /// <summary>
    /// Класс для заполнения бд Action's
    /// </summary>
    public class ActionDataFactory
    {
        private readonly DatabaseContext _context;

        public ActionDataFactory()
        {
            _context = TestInitializer.Provider.GetService<DatabaseContext>();
        }

        //Заполняет бд action's
        public async Task CreateActions()
        {
            var user = await _context.Users.FirstAsync();
            var actions = new List<ActionLog>
            {
                new ActionLog(user.UserId, "ValueContoller", "getValue"),
                new ActionLog(user.UserId, "TestContoller", "TestValue")
            };
            await _context.ActionsLogs.AddRangeAsync(actions);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task Dispose()
        {
            var actions = await _context.ActionsLogs.ToListAsync();
            _context.ActionsLogs.RemoveRange(actions);
            await _context.SaveChangesAsync();
        }
    }
}
