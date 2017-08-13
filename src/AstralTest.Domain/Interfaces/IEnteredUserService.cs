using System;
using System.Collections.Generic;
using System.Text;
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
        List<EnteredUser> EnteredUsers { get; }

        /// <summary>
        /// Добавляет пользователя, который входил в приложение
        /// </summary>
        /// <param name="idUser">Id пользователя</param>
        /// <returns></returns>
        Task<Guid> AddAsync(Guid idUser);

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
        Task<List<EnteredUser>> GetAsync();

    }
}
