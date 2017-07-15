using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Model
{
    /// <summary>
    /// Класс для изменения заметок в контролере
    /// </summary>
    public class NoteEditModel
    {
        /// <summary>
        /// Id заметки
        /// </summary>
        [Required]
        public Guid idNote { get; set; }

        /// <summary>
        /// Текста заметки
        /// </summary>
        [Required]
        public string Text { get; set; }
    }
}
