using System;
using System.Collections.Generic;
using System.Text;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AstralTest.Tests.Domain.Entities.Factory
{
    /// <summary>
    /// Класс для создания информации информации для логов
    /// </summary>
    public class InfoAboutActionDataFactory
    {

        private readonly DatabaseContext _context;

        public InfoAboutActionDataFactory()
        {
            _context = TestInitializer.Provider.GetService<DatabaseContext>();
        }

        //Заполняет бд action's
        public async Task CreateInfoActions()
        {
            var action = await _context.ActionsLogs.FirstAsync();
            var actions = new List<ParametrsAction>
            {
                new ParametrsAction(action.Id,"paramets1"),
                new ParametrsAction(action.Id,"parametrs2")
            };
            await _context.ParametrsActions.AddRangeAsync(actions);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task Dispose()
        {
            var infos = await _context.ParametrsActions.ToListAsync();
            _context.ParametrsActions.RemoveRange(infos);
            await _context.SaveChangesAsync();
        }
    }
}
