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
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public IEnumerable<User> Users
        {
            get
            {
                return _context.Users.Include(x => x.Notes).ToList();
            }
        }

        public UserService(DatabaseContext context, UserManager<User> userManage,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManage;
            _roleManager = roleManager;
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
                var resultRole = await GetRoleOrDefault(user.RoleName);
                await _userManager.AddToRoleAsync(resultUser, resultRole);
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
            var resultRole = await GetRoleOrDefault(user.RoleName);
            await _userManager.AddToRoleAsync(result, resultRole);
           
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
            var result =await _context.Users.Include(x=>x.Notes).ToListAsync();
            return result;
        }

        /// <summary>
        /// Изменяет пароль пользователя.Возвращает 1 если пароль успешно сменён, 0 если возникли ошибки
        /// </summary>
        /// <param name="editModel">Модель содержащая oldPasswordn и newPassword</param>
        ///  /// <param name="userName">Имя пользователя</param>
        /// <returns></returns>
        public async Task<int> EditPasswordAsync(string userName,EditPasswordModel model)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user!=null)
            {
                var result =await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if(result.Succeeded)
                {
                    return await Task.FromResult(1);
                }
            }
            return await Task.FromResult(0);
        }


        /// <summary>
        /// Проверяет, существует ли такая роль, возвращает название роли если существует, или стандартное название роли user.
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <returns></returns>
        private async Task<string> GetRoleOrDefault(string roleName)
        {
            if(await _roleManager.RoleExistsAsync(roleName))
            {
                return roleName;
            }
            if(!await _roleManager.RoleExistsAsync("user"))
            {
               await _roleManager.CreateAsync(new IdentityRole("user"));
            }
            return "user";
        }
    }
}
