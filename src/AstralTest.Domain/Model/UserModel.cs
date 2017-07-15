using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Model
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
        public string Name { get; set; }
    }
}
