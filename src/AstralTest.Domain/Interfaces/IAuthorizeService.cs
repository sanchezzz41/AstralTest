using AstralTest.Domain.Entities;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для авторизации
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Возвращает пользователя, если он авторизовался, иначе null
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        User Authorization(string userName, string password);
    }
}
