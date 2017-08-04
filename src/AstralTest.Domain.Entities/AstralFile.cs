using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс, предоставляющий файл 
    /// </summary>
    public class AstralFile
    {
        /// <summary>
        /// Id файла, под ним файл храниться в локальном хранилище
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FileId { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        [Required]
        public string TypeFile { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        [Required]
        public string NameFile { get; set; }

        /// <summary>
        /// Время создания файла
        /// </summary>
        public DateTime CreatedTime { get; set; }

        [ForeignKey("TaskId")]
        public Guid TaskId { get; set; }
        
        /// <summary>
        /// Задача, к которой принадлежит файл
        /// </summary>
        public virtual UserTask MasterTask { get; set; }

        /// <summary>
        /// Иницилизирует новый экземпрял AstralFile
        /// </summary>
        public AstralFile()
        {
            FileId = Guid.NewGuid();
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        ///  Иницилизирует новый экземпрял AstralFile
        /// </summary>
        /// <param name="typeFile">Тип файла</param>
        /// <param name="taskId">Id задачи, которой принадлежит файл</param>
        /// <param name="nameFile">Название файла</param>
        public AstralFile( Guid taskId, string typeFile,string nameFile)
        {
            FileId = Guid.NewGuid();
            TypeFile = typeFile;
            NameFile = nameFile;
            TaskId = taskId;
            CreatedTime = DateTime.Now;
        }
    }
}
