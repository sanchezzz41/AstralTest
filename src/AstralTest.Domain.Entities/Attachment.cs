using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс который связывает задачу и файл
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Id задачи
        /// </summary>
        [ForeignKey(nameof(MasterTask))]
        public Guid TaskId { get; set; }

        /// <summary>
        /// Id файла
        /// </summary>
        [ForeignKey(nameof(MasterFile))]
        public Guid FileId { get; set; }

        public UserTask MasterTask { get; set; }
        public File MasterFile { get; set; }

        /// <summary>
        /// Иницилизирует новый экземпрял класса
        /// </summary>
        /// <param name="taskId">Id задачаи</param>
        /// <param name="fileId">Id файла</param>
        public Attachment(Guid taskId, Guid fileId)
        {
            TaskId = taskId;
            FileId = fileId;
        }
    }
}
