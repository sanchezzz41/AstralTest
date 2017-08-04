using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;
using Microsoft.AspNetCore.Http;
using File = AstralTest.Domain.Entities.File;

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
        /// <returns></returns>
        Task<Guid> AddAsynce(IFormFile formFile);

        /// <summary>
        /// Возвращает модель в которой находится файл
        /// </summary>
        /// <param name="idFile">Id файла, который надо вернуть</param>
        /// <returns></returns>
        Task<FileModel> GetFileAsync(Guid idFile);

        /// <summary>
        /// Удаляет файд
        /// </summary>
        /// <param name="idFile">Id файла, по которому будет производиться удаление</param>
        /// <returns></returns>
        Task DeleteAsync(Guid idFile);

        /// <summary>
        /// Возвращает из бд все записи о файлах
        /// </summary>
        /// <returns></returns>
        Task<List<File>> GetInfoAboutAllFilesAsync();
    }
}
