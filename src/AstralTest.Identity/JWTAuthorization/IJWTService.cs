using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AstralTest.Identity.JWTModel
{
    /// <summary>
    /// Интерфейс для генирации JWT токена
    /// </summary>
    public interface IJWTService
    {
        /// <summary>
        /// Создаёт токен
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        Task<Token> CreateToken(string userName, string password);
    }
}
