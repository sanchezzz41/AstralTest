using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для смены пароля
    /// </summary>
    public class EditPasswordModel
    {
        [Required]
        [Display(Name ="Старый пароль")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name ="Новый пароль")]
        public string NewPassword { get; set; }
                                    
    }
}
