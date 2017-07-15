using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для работы с заметкой в контроллере
    /// </summary>
    public class NoteModel
    {

        /// <summary>
        /// Id заметки
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID пользователя
        /// </summary>       
        public Guid IdMaster { get; set; }

        /// <summary>
        /// Текст заметки
        /// </summary>
        [Required]
        public string Text { get; set; }
    }
}
