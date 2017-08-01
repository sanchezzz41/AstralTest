using System.Linq;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс для авторизации пользователя
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthorizationService(IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Возвращает пользователя, если он успешно прошёл авторизацию, в противном случае null
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public User Authorization(string userName, string password)
        {
            if (userName == null)
            {
                return null;
            }
            var user = _userService.Users.SingleOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return null;
            }
            var resultHash = _passwordHasher.HashPassword(user, password);

            if (resultHash != user.PasswordHash)
            {
                return null;
            }
            return user;
        }
    }
}
