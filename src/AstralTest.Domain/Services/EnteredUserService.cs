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
        public List<EnteredUser> EnteredUsers {
            get
            {
                return _context.EnteredUsers
                    .Include(x => x.User)
                    .Include(x => x.InfoAboutEnteredUsers)
                    .ToList();
            } }

        /// <summary>
        /// Добавляет пользователя, который входил в приложение
        /// </summary>
        /// <param name="idUser">Id пользователя</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(Guid idUser)
        {
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.UserId == idUser);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с таким id {idUser} не существует.");
            }
            var resuleEnteredUser = new EnteredUser(resultUser.UserId);
            await _context.EnteredUsers.AddAsync(resuleEnteredUser);
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
            var resultEnteredUser = await _context.EnteredUsers.SingleOrDefaultAsync(x => x.Id == idEnteredUser);
            if (resultEnteredUser == null)
            {
                throw new NullReferenceException(
                    $"Информации о пользователе по такому id {idEnteredUser} не существует.");
            }
            _context.EnteredUsers.Remove(resultEnteredUser);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список входивших пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnteredUser>> GetAsync()
        {
            return await _context.EnteredUsers
                .Include(x => x.User)
                .Include(x => x.InfoAboutEnteredUsers)
                .ToListAsync(); ;
        }
    }
}
