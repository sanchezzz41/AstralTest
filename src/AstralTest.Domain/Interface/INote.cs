using AstralTest.DataDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AstralTest.Domain.Interface
{
    /// <summary>
    /// Интерфейс для работы с заметками пользователя, включает стандартные операции CRUD
    /// </summary>
    public interface INote
    {
        IEnumerable<Note> Notes { get; }

        /// <summary>
        /// Добавляет заметку в бд
        /// </summary>
        /// <param name="user">Владелек заметки</param>
        /// <param name="note">Заетка для добавления в бд</param>
        /// <returns></returns>
        Task<Guid> AddAsync(User user,Note note);

        /// <summary>
        /// Удаляет заметку из бд
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        Task DeleteAsync(Note note);

        /// <summary>
        /// Изменяет заметку
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        Task EditAsync(Note note);

        /// <summary>
        /// Получает заметки из БД
        /// </summary>
        /// <returns></returns>
        Task<List<Note>> GetAsync();

    }
}
