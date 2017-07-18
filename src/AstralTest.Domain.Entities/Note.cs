﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющей заметку дял пользователя
    /// </summary>
    public class Note
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Текст в заметке
        /// </summary>
        [Required]
        public string Text { get; set; }


        [ForeignKey(nameof(Master))]
        public string IdUser { get; set; }


        /// <summary>
        ///  Пользователь 
        /// </summary>  
        public virtual User Master { get; set; }

        ///// <summary>
        ///// Иницилизирует класс для заметки(Id автоматически создаётся)
        ///// </summary>
        //public Note()
        //{
        //    Id = Guid.NewGuid();
        //}

        ///// <summary>
        ///// Иницилизирует класс для заметки(Id создаётся автоматически)
        ///// </summary>
        ///// <param name="text">Текст заметки</param>
        ///// <param name="idMaster">Id владельца заметки</param>
        //public Note(string text, string idMaster)
        //{
        //    Id = Guid.NewGuid();
        //    Text = text;
        //    IdUser = idMaster;
        //}
    }
}
