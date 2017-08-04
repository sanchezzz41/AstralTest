using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Сервер для привязки файла к задаче
    /// </summary>
    public interface IAttachmentsService
    {
        /// <summary>
        /// Прикрепляет файл к задаче, используя id обоих
        /// </summary>
        /// <param name="taskId">Id задачи</param>
        /// <param name="fileId">Id файла</param>
        /// <returns></returns>
        Task AttachFileToTaskAsync(Guid taskId, Guid fileId);

        /// <summary>
        /// Прикрепляет файл к задаче
        /// </summary>
        /// <param name="taskId">Id задачи</param>
        /// <param name="file">Файл, который будет прикреплён к задаче</param>
        /// <returns></returns>
        Task AttachFileToTaskAsync(Guid taskId, IFormFile file);

        /// <summary>
        /// Удаляет привязку между задачей и файлом
        /// </summary>
        /// <param name="taskId">Id задачи</param>
        /// <param name="fileId">Id файла</param>
        /// <returns></returns>
        Task DeleteAtachmentAsync(Guid taskId, Guid fileId);

    }
}
