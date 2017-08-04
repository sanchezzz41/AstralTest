using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.FileStore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс для работы с файлами
    /// </summary>
    public class FileService:IFileService
    {
        private readonly DatabaseContext _context;
        private readonly IFileStore _fileStore;
        public FileService(DatabaseContext context,IFileStore filestroe)
        {
            _context = context;
            _fileStore = filestroe;
        }

        /// <summary>
        /// Добавляет файл в бд и в локальное хранилище
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsynce(IFormFile formFile, Guid taskId)
        {
            if (formFile == null)
            {
                throw new Exception("Файла для добавления нету.");
            }
            var resultTask = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == taskId);
            if (resultTask == null)
            {
                throw new Exception("Задачи с таким id нету.");
            }

            var result = new AstralFile(taskId, formFile.ContentType, formFile.FileName);
            await _context.Files.AddAsync(result);

            var resultStream = new MemoryStream();
            await formFile.CopyToAsync(resultStream);
            await _fileStore.Create(resultStream, result.FileId.ToString());

            await _context.SaveChangesAsync();

            return result.FileId;
        }

        /// <summary>
        /// Возвращает модель в которой находится файл
        /// </summary>
        /// <param name="idFile">Id файла, который надо вернуть</param>
        /// <returns></returns>
        public async Task<FileModel> GetFile(Guid idFile)
        {
            var resultFile = await _context.Files.SingleOrDefaultAsync(x => x.FileId == idFile);
            if (resultFile == null)
            {
                throw new Exception("Файла с таким id нету.");
            }
            var resultMass =await _fileStore.Upload(resultFile.FileId.ToString());
            if (resultMass == null && resultMass.Length == 0)
            {
                throw new Exception("Файла с таким id нету в хранилище.");
            }

            var result = new FileModel
            {
                Bytes = resultMass,
                NameFile = resultFile.NameFile,
                TypeFile=resultFile.TypeFile
            };
            return result;
        }
    }
}
