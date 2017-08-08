using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Utilits;
using AstralTest.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AstralTest.Domain.Services
{

    /// <summary>
    /// Класс для работы с пользоваталем
    /// </summary>
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;


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
                throw new NullReferenceException($"Ссылка на пользователя указывате на Null.");
            }
            //Передаем пароль сразу с солью(сначала пароль, потом соль)!!!
            var passwordSalt = Randomizer.GetString(8);
            var passworhHash = _passwordHasher.HashPassword(null, userModel.Password + passwordSalt);
            var resultUser=new User(userModel.UserName,userModel.Email,passwordSalt,passworhHash,userModel.RoleId);
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
                throw new NullReferenceException("Ссылка на пользователя равна null");
            }
            var result = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            if (result == null)
            {
                throw new NullReferenceException($"Пользователя с таким id{id} нету");
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
            if (user.RoleId == RolesOption.Admin)
            {
                result.RoleId = user.RoleId;
            }
            else
            {
                result.RoleId = RolesOption.User;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет пользователя из БД
        /// </summary>
        /// <param name="idUser">Пользователь для удаления(у пользователя можно указать только id)</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idUser)
        {
            var result = await _context.Users.SingleOrDefaultAsync(x => x.UserId == idUser);
            if (result == null)
            {
                throw new NullReferenceException($"Пользователя с таким id{idUser} нету");
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
            var result = await _context.Users
                .Include(x => x.Notes)
                .Include(x=>x.Role)
                .Include(x=>x.TasksContainers)
                .ToListAsync();
            return result;
        }
    }
}
