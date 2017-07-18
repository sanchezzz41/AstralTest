using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для работы с пользователем в контролере
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        [Display(Name="Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]   
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        ///// <summary>
        ///// Id пользователя
        ///// </summary>
        //public Guid Id { get; set; }
    }
}
