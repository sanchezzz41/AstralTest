using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Model
{
    /// <summary>
    /// Класс для работы с заметкой в контроллере
    /// </summary>
    public class NoteModel
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        [Required]
        public Guid IdMaster { get; set; }

        /// <summary>
        /// Текст заметки
        /// </summary>
        [Required]
        public string Text { get; set; }
    }
}
