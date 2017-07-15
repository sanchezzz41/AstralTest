using AstralTest.DataDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Interface
{

    /// <summary>
    /// Интерфейс для работы с пользователем, включает стандартные операции CRUD
    /// </summary>
    public interface IUser
    {
        IEnumerable<User> Users { get; }

        /// <summary>
        /// Добавления пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id пользователя</returns>
        Task<Guid> AddAsync(User user);

        /// <summary>
        /// Удаляет пользователя из БД
        /// </summary>
        /// <param name="user">Пользователь для удаления(у пользователя можно указать только id)</param>
        /// <returns></returns>
        Task DeleteAsync(User user);

        /// <summary>
        /// Изменяет пользователя
        /// </summary>
        /// <param name="user">Пользователь с тем же Id, но с новыми данными</param>
        /// <returns></returns>
        Task EditAsync(User user);

        /// <summary>
        /// Получает пользователей из БД 
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAsync();
    }
}
