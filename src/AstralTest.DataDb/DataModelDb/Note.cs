using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.DataDb
{
    /// <summary>
    /// Класс предоставляющей заметку дял пользователя
    /// </summary>
    public class Note
    {

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Текст в заметке
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Id Пользоваетля
        /// </summary>
        [ForeignKey(nameof(Master))]
        public Guid? MasterId { get; set; }

        /// <summary>
        ///  Пользователь 
        /// </summary>  
        public virtual User Master { get; set; }

        /// <summary>
        /// Иницилизирует класс для заметки(Id автоматически создаётся)
        /// </summary>
        public Note()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Иницилизирует класс для заметки(Id создаётся автоматически)
        /// </summary>
        /// <param name="text">Текст заметки</param>
        /// <param name="idMaster">Id владельца заметки</param>
        public Note(string text, Guid idMaster)
        {
            Id = Guid.NewGuid();
            Text = text;
            MasterId = idMaster;
        }
    }
}
