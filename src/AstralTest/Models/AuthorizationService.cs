using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace AstralTest.Models
{
    /// <summary>
    /// Класс для авторизации пользователя
    /// </summary>
    public class AuthorizationService
    {
        private IUserService _userService;
        private IPasswordHasher<User> _passwordHasher;

        public AuthorizationService(IUserService context, IPasswordHasher<User> passwordHasher)
        {
            _userService = context;
            _passwordHasher = passwordHasher;
        }

        public User Authorization(string userName, string password)
        {
            if (userName == null)
            { return null; }
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
