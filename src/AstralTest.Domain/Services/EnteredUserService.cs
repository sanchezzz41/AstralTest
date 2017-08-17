using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс для работы с пользователями, которые входили в приложение
    /// </summary>
    public class EnteredUserService : IEnteredUserService
    {
        private readonly DatabaseContext _context;

        public EnteredUserService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Содержит входивших пользователей
        /// </summary>
        public List<ActionLog> EnteredUsers {
            get
            {
                return _context.ActionsLogs
                    .Include(x => x.User)
                    .Include(x => x.InfoAboutEnteredUsers)
                    .ToList();
            } }

        /// <summary>
        /// Добавляет пользователя, который входил в приложение
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(string userName)
        {

            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с таким именем {userName} не существует.");
            }
            var testEntuser = await _context.ActionsLogs.SingleOrDefaultAsync(x => x.User.UserName == userName);
            if (testEntuser != null)
            {
                return testEntuser.Id;
            }
            var resuleEnteredUser = new ActionLog(resultUser.UserId);
            await _context.ActionsLogs.AddAsync(resuleEnteredUser);
            await _context.SaveChangesAsync();
            return resuleEnteredUser.Id;
        }

        /// <summary>
        /// Удаляет пользователя, который обращался к приложению
        /// </summary>
        /// <param name="idEnteredUser"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idEnteredUser)
        {
            var resultEnteredUser = await _context.ActionsLogs.SingleOrDefaultAsync(x => x.Id == idEnteredUser);
            if (resultEnteredUser == null)
            {
                throw new NullReferenceException(
                    $"Информации о пользователе по такому id {idEnteredUser} не существует.");
            }
            _context.ActionsLogs.Remove(resultEnteredUser);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список входивших пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<List<ActionLog>> GetAsync()
        {
            return await _context.ActionsLogs
                .Include(x => x.User)
                .Include(x => x.InfoAboutEnteredUsers)
                .ToListAsync(); ;
        }
    }
}
