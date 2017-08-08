using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;

namespace AstralTest.XSSFConverter
{
    /// <summary>
    /// Интерфейс для конвертации списков в Excel документ
    /// </summary>
    public interface IXssfConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<byte[]> UsersConvertToXssfAsync(IEnumerable<User> list);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>

        Task<byte[]> NotesConvertToXssfAsync(IEnumerable<Note> list);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>

        Task<byte[]> TaskContainersConvertToXssfAsync(IEnumerable<TasksContainer> list);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>

        Task<byte[]> FilesConvertToXssfAsync(IEnumerable<File> list);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<byte[]> TasksConvertToXssfAsync(IEnumerable<UserTask> list);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<byte[]> AttachmentsConvertToXssfAsync(IEnumerable<Attachment> list);
    }
}
