using AstralTest.DataDb;
using AstralTest.Domain.Interface;
using AstralTest.ContextDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace AstralTest.Domain.Service
{

    /// <summary>
    /// Класс для работы с пользоваталем
    /// </summary>
    public class UserService : IUser
    {
        private AstralContext _context { get; }

        public IEnumerable<User> Users
        {
            get
            {
                return _context.Users.Include(x => x.Notes).ToList();
            }
        }

        public UserService(AstralContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавления пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id пользователя</returns>
        public async Task<Guid> AddAsync(User user)
        {

            if (user == null)
            {
                throw new Exception("User is null");
            }

            var promUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == user.Id);

            if (promUser != null)
            {
                throw new Exception("User with same Id is exist");
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        /// <summary>
        /// Изменяет пользователя
        /// </summary>
        /// <param name="user">Пользователь с тем же Id, но с новыми данными</param>
        /// <returns></returns>
        public async Task EditAsync(User user)
        {
            if (user == null)
            {
                throw new Exception("User is null");
            }
            var result = await _context.Users.SingleOrDefaultAsync(x => x.Id == user.Id);

            if (result == null)
            {
                throw new Exception("User with same Id is not exist");
            }

            result.Name = user.Name;
            _context.Users.Attach(result);
            _context.Entry(result).Property(x => x.Name).IsModified = true;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет пользователя из БД
        /// </summary>
        /// <param name="user">Пользователь для удаления(у пользователя можно указать только id)</param>
        /// <returns></returns>
        public async Task DeleteAsync(User user)
        {
            if (user == null)
            {
                throw new Exception("User is null");
            }

            var result = await _context.Users.SingleOrDefaultAsync(x => x.Id == user.Id);
            if (result == null)
            {
                throw new Exception("User with same Id is not exist");

            }
            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает пользователей из БД 
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAsync()
        {
            var result =await _context.Users.ToListAsync();
            return result;
        }
    }
}
