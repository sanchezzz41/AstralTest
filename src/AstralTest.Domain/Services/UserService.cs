﻿using Microsoft.EntityFrameworkCore;
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
        private UserManager<User> _userManager;

        public IEnumerable<User> Users
        {
            get
            {
                return _context.Users.Include(x => x.Notes).ToList();
            }
        }

        public UserService(DatabaseContext context, UserManager<User> userManage)
        {
            _context = context;
            _userManager = userManage;
        }

        /// <summary>
        /// Добавления пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id пользователя</returns>
        public async Task<Guid> AddAsync(UserRegisterModel user)
        {
            if (user == null)
            {
                throw new Exception("User is null");
            }
            //Пользователь, которого добавят в бд
            var resultUser = new User { UserName = user.UserName,
            Email=user.Email};

            if (_context.Users.Any(x=>x.Id==resultUser.Id))
            {       
                throw new Exception("User with same Id is exist");
            }

            var createUser = await _userManager.CreateAsync(resultUser, user.Password);

            if (createUser.Succeeded)
            {
                //TODO: Прикрутить проверку, на существование такой роли
                await _userManager.AddToRoleAsync(resultUser, user.RoleName);
                var idstring = await _userManager.GetUserIdAsync(resultUser);  
                return Guid.Parse(idstring);
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Изменяет имя пользователя и пароль
        /// </summary>
        /// <param name="user">Пользователь с тем же Id, но с новыми данными</param>
        /// <returns></returns>
        public async Task EditAsync(EditUserModel user, string id)
        {

            if (user == null && id == null)
            {
                throw new Exception("User is null");
            }
            var result = await _userManager.FindByIdAsync(id);
            if (result==null)
            {
                throw new Exception("User with same Id is not exist");
            }
            //TODO:Скорей всего ещё надо добавить изменение роли
            result.Email = user.Email;
            result.UserName = user.UserName;
            await _userManager.UpdateAsync(result);
        }

        /// <summary>
        /// Удаляет пользователя из БД
        /// </summary>
        /// <param name="user">Пользователь для удаления(у пользователя можно указать только id)</param>
        /// <returns></returns>
        public async Task DeleteAsync(string idUser)
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
