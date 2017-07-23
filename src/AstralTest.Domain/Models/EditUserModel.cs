using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для изменения пользователя
    /// </summary>
    public class EditUserModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Display(Name="Имя пользователя")]
        public string UserName { get; set; }

        /// <summary>
        /// Email 
        /// </summary>
        [Display(Name = "Email")]   
        public string Email { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Display(Name ="Роль")]
        public string RoleName { get; set; }

        //Если надо будет изменять пароль
        //public string Password { get; set; }

        ///// <summary>
        ///// Id пользователя
        ///// </summary>
        //public Guid Id { get; set; }
    }
}
