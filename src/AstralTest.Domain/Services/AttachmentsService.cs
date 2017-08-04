using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс позволяющий прикреплять файлы к задачам
    /// </summary>
    public class AttachmentsService : IAttachmentsService
    {
        private readonly DatabaseContext _context;
        private readonly IFileService _fileService;

        public AttachmentsService(DatabaseContext context, IFileService service)
        {
            _context = context;
            _fileService = service;
        }

        /// <summary>
        /// Прикрепляет файл к задаче, используя id обоих
        /// </summary>
        /// <param name="taskId">Id задачи</param>
        /// <param name="fileId">Id файла</param>
        /// <returns></returns>
        public async Task AttachFileToTaskAsync(Guid taskId, Guid fileId)
        {
            var resultTask = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == taskId);
            if (resultTask == null)
            {
                throw new Exception("Задачи с таким id не существует");
            }
            var resultFile = await _context.Files.SingleOrDefaultAsync(x => x.FileId == fileId);
            if (resultFile == null)
            {
                throw new Exception("Файла с таким id не существует");
            }
            var result = new Attachment(resultTask.TaskId,resultFile.FileId);
            await _context.Attachments.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Прикрепляет файл к задаче
        /// </summary>
        /// <param name="taskId">Id задачи</param>
        /// <param name="file">Файл, который будет прикреплён к задаче</param>
        /// <returns></returns>
        public async Task AttachFileToTaskAsync(Guid taskId, IFormFile file)
        {
            var resultTask = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == taskId);
            if (resultTask == null)
            {
                throw new Exception("Задачи с таким id не существует");
            }
            var fileId = await _fileService.AddAsynce(file);
            var result = new Attachment(resultTask.TaskId, fileId);
            await _context.Attachments.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет привязку между задачей и файлом
        /// </summary>
        /// <param name="taskId">Id задачи</param>
        /// <param name="fileId">Id файла</param>
        /// <returns></returns>
        public async Task DeleteAtachmentAsync(Guid taskId, Guid fileId)
        {
            var resultAttachment = await _context.Attachments
                .SingleOrDefaultAsync(x => x.TaskId == taskId && x.FileId == fileId);
            if (resultAttachment == null)
            {
                throw new Exception("Привязки с такими id не существует");
            }
            _context.Attachments.Remove(resultAttachment);
            await _context.SaveChangesAsync();
        }
    }
}
