using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс модель для входа пользователя в приложение
    /// </summary>
    public class LoginViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        [Required]
        public string Login { get; set; }
        public string ReturnUrl { get; set; }
    }
}
