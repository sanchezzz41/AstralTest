using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AstralTest.Domain.Services
{
    public class InfoActionService : IInfoActionService
    {
        /// <summary>
        /// Содержит информацию о том, к чему обращались пользователи
        /// </summary>
        public List<ParametrsAction> InfoUsers
        {
            get
            {
                return _context.ParametrsActions
                    .Include(x => x.Action)
                    .ToList();
            }
        }

        private readonly DatabaseContext _context;

        public InfoActionService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет информацию о том, к чему обращается пользователь
        /// </summary>
        /// <param name="paramets">Параметры метода</param>
        /// <param name="idAction">Id обращаегося пользователя</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(string paramets, Guid idAction)
        {
            var resultEntUser = await _context.ActionsLogs.SingleOrDefaultAsync(x => x.Id == idAction);

            if (resultEntUser == null)
            {
                throw new NullReferenceException(
                    $"Пользователя, который обращается к приложению с таким Id{idAction} не существует.");
            }
            var result = new ParametrsAction(idAction, paramets);
            await _context.ParametrsActions.AddAsync(result);
            await _context.SaveChangesAsync();
            return result.Id;
        }


        /// <summary>
        /// Возвращает всю информацию о том, кто и когда обращался к приложению
        /// </summary>
        /// <returns></returns>
        public async Task<List<ParametrsAction>> GetAsync()
        {
            return await _context.ParametrsActions
                .Include(x => x.Action)
                .ToListAsync();
        }
    }
}

