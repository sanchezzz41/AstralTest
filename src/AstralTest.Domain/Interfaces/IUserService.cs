using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Interfaces
{

    /// <summary>
    /// Интерфейс для работы с пользователем, включает стандартные операции CRUD
    /// </summary>
    public interface IUserService
    {
        IEnumerable<User> Users { get; }

        /// <summary>
        /// Добавления пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id пользователя</returns>
        Task<Guid> AddAsync(UserModel user);

        /// <summary>
        /// Удаляет пользователя из БД
        /// </summary>
        /// <param name="idUser">id пользователя</param>
        /// <returns></returns>
        Task DeleteAsync(Guid idUser);

        /// <summary>
        /// Изменяет пользователя
        /// </summary>
        /// <param name="user">Пользователь с тем же Id, но с новыми данными</param>
        /// <returns></returns>
        Task EditAsync(UserModel user);

        /// <summary>
        /// Получает пользователей из БД 
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAsync();
    }
}
