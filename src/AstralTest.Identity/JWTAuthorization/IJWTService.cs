using System.Threading.Tasks;

namespace AstralTest.Identity.JWTAuthorization
{
    /// <summary>
    /// Интерфейс для генирации JWT токена
    /// </summary>
    public interface IJwtService
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
