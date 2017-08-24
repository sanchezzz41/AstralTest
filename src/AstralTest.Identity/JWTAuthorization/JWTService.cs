using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AstralTest.Identity.JWTAuthorization
{
    /// <summary>
    /// Сервис реализующий интерфейс IJWTService 
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly TokenOption _tokenOption;

        public JwtService(DatabaseContext context, IPasswordHasher<User> passwordHasher, TokenOption tokenOption)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenOption = tokenOption;
        }

        /// <summary>
        /// Создаёт токен
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        public async Task<Token> CreateToken(string userName, string password)
        {
            var identity = await GetIdentity(userName, password);

            var now = DateTime.UtcNow;
            var securutyKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenOption.Key));
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                audience: _tokenOption.Audince,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_tokenOption.LifeTime)),
                signingCredentials: new SigningCredentials(securutyKey,
                    SecurityAlgorithms.HmacSha256));
            var tokenHandler = new JwtSecurityTokenHandler();
            var resultToken = tokenHandler.WriteToken(jwt);

            var result = new Token
            {
                TokenContent = resultToken,
                TokenType = "Bearer"
            };
            return result;
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
            if (user == null)
            {
                throw new NullReferenceException($"Пользователя под таким ником {username} не существуте.");
            }
            var resultHash = _passwordHasher.HashPassword(user, password);
            if (user.PasswordHash != resultHash)
            {
                throw new InvalidOperationException($"Пароль {password} не верный.");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString())
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
