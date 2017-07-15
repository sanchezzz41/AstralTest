using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Database;
using AstralTest.Domain.Models;

namespace AstralTest.Domain.Service
{

    /// <summary>
    /// Класс для работы с пользоваталем
    /// </summary>
    public class UserService : IUserService
    {
        private DatabaseContext _context { get; }

        public IEnumerable<User> Users
        {
            get
            {
                return _context.Users.Include(x => x.Notes).ToList();
            }
        }

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавления пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id пользователя</returns>
        public async Task<Guid> AddAsync(UserModel user)
        {
            if (user == null && user.Id==null)
            {
                throw new Exception("User is null");
            }

            var result = new User { Name = user.Name };

            if (_context.Users.Any(x=>x.Id==result.Id))
            {       
                throw new Exception("User with same Id is exist");
            }     
            
            await _context.Users.AddAsync(result);
            await _context.SaveChangesAsync();
            return result.Id;
        }

        /// <summary>
        /// Изменяет пользователя
        /// </summary>
        /// <param name="user">Пользователь с тем же Id, но с новыми данными</param>
        /// <returns></returns>
        public async Task EditAsync(UserModel user)
        {
            if (user == null && user.Id == null)
            {
                throw new Exception("User is null");
            }
            var result = await _context.Users.SingleOrDefaultAsync(x => x.Id == user.Id);

            if (result == null)
            {
                throw new Exception("User with same Id is not exist");
            }

            result.Name = user.Name;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет пользователя из БД
        /// </summary>
        /// <param name="user">Пользователь для удаления(у пользователя можно указать только id)</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idUser)
        {
            if (idUser == null)
            {
                throw new Exception("User is null");
            }

            var result = await _context.Users.SingleOrDefaultAsync(x => x.Id == idUser);
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
