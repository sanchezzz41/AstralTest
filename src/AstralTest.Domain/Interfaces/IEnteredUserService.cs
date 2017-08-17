using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интферфейс для работы с пользователями, которые обращаются к приложение
    /// </summary>
    public interface IEnteredUserService
    {
        /// <summary>
        /// Содержит входивших пользователей
        /// </summary>
        List<ActionLog> EnteredUsers { get; }

        /// <summary>
        /// Добавляет пользователя, который входил в приложение
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <returns></returns>
        Task<Guid> AddAsync(string userName);

        /// <summary>
        /// Удаляет пользователя, который обращался к приложению
        /// </summary>
        /// <param name="idEnteredUser"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid idEnteredUser);

        /// <summary>
        /// Возвращает список входивших пользователей
        /// </summary>
        /// <returns></returns>
        Task<List<ActionLog>> GetAsync();
    }
}
