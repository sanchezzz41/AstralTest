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
        /// Текст заметки
        /// </summary>
        [Required]
        public string Text { get; set; }
    }
}
