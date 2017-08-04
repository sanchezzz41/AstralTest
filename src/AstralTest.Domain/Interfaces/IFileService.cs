using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с файлами
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Добавляет файл в бд и в локальное хранилище
        /// </summary>
        /// <param name="formFile">Интерфейс предоставляющий файл</param>
        /// <param name="taskId">Id задачи, к которой будет закреплён файл</param>
        /// <returns></returns>
        Task<Guid> AddAsynce(IFormFile formFile, Guid taskId);

        /// <summary>
        /// Возвращает модель в которой находится файл
        /// </summary>
        /// <param name="idFile">Id файла, который надо вернуть</param>
        /// <returns></returns>
        Task<FileModel> GetFile(Guid idFile);

    }
}
