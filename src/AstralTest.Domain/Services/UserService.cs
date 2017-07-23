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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AstralTest.Domain.Service
{

    /// <summary>
    /// Класс для работы с пользоваталем
    /// </summary>
    public class UserService : IUserService
    {
        private DatabaseContext _context { get; }
        IPasswordHasher<User> _passwordHasher;


        public IEnumerable<User> Users
        {
            get
            {
                return _context.Users.Include(x => x.Notes).ToList();
            }
        }

        public UserService(DatabaseContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Добавления пользователя в БД
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Id пользователя</returns>
        public async Task<Guid> AddAsync(UserRegisterModel userModel)
        {
            if (userModel == null)
            {
                throw new Exception("User is null");
            }
            //Создаём пользователя
            var resultUser = new User
            {
                UserName = userModel.UserName,
                Email = userModel.Email
            };
            if (_context.Users.Any(x => x.UserId == resultUser.UserId))
            {
                throw new Exception("User with same Id is exist");
            }
            //Проверяем наличие роли, и если есть добавляем
            var resRole = await _context.Roles.SingleOrDefaultAsync(x => x.RoleName == userModel.RoleName.ToLower());
            if (resRole != null)
            {
                resultUser.RoleId = resRole.RoleId;
            }
            else
            {
                resRole = await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == RolesAuthorize.user);
                resultUser.RoleId = resRole.RoleId;
            }
            //Создаем хэш пароля и добавляем его пользователю
            var passworhHash = _passwordHasher.HashPassword(resultUser, userModel.Password);
            resultUser.PasswordHash = passworhHash;
            //Сохраняем пользователя
            await _context.AddAsync(resultUser);
            await _context.SaveChangesAsync();

            return resultUser.UserId;
        }

        /// <summary>
        /// Изменяет имя пользователя и пароль
        /// </summary>
        /// <param name="user">Модель пользователя для изменения. Можно указывать не все параметры.</param>
        /// <param name="id">Id пользователя.</param>
        /// <returns></returns>
        public async Task EditAsync(EditUserModel user, Guid id)
        {

            if (user == null || id == null || id == Guid.Empty)
            {
                throw new Exception("User is null");
            }
            var result = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            if (result == null)
            {
                throw new Exception("User with same Id is not exist");
            }
            //Обновление значений пользователя

            //Обновления Email
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                result.Email = user.Email;
            }
            //Обновления имени
            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                result.UserName = user.UserName;
            }

            //Обновления пароля, если понадобиться
            //if (!string.IsNullOrWhiteSpace(user.Password))
            //{
            //    result.PasswordHash = _passwordHasher.HashPassword(result, user.Password);
            //}

            //Обновления роли
            if (!string.IsNullOrWhiteSpace(user.RoleName))
            {
                var newRole = await _context.Roles.SingleOrDefaultAsync(x => x.RoleName == user.RoleName.ToLower());
                if (newRole != null)
                {
                    result.RoleId = newRole.RoleId;
                }
                else
                {
                    newRole = await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == RolesAuthorize.user);
                    result.RoleId = newRole.RoleId;
                }
            }

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

            var result = await _context.Users.SingleOrDefaultAsync(x => x.UserId == idUser);
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
            var result = await _context.Users.Include(x => x.Notes).Include(x=>x.Role).ToListAsync();
            return result;
        }
    }
}
