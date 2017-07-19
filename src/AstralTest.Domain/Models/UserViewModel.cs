using AstralTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для отображения пользователей
    /// </summary>
    public class UserViewModel
    {
        [Display(Name ="Имя пользователя")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email пользователя")]
        public string Email { get; set; }

        
        public string Id { get; set; }

        
        public List<Note> Notes { get; set; }
    }
}
