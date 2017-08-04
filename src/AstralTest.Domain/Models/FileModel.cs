using System;
using System.Collections.Generic;
using System.Text;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Модель файла, которая будет отправляться в respons'e
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// Массив байтов
        /// </summary>
        public byte[] Bytes { get; set; }
        
        /// <summary>
        /// Тип файла
        /// </summary>
        public string TypeFile { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string NameFile { get; set; }
    }
}
