using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AstralTest.Domain.Entities
{

    /// <summary>
    /// Предоставляет класс для пользоваетля
    /// </summary>
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Имя(Nick name)
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Соль для хэша
        /// </summary>
        public string PasswordSalt { get; internal set; }
        
        /// <summary>
        /// Id роли
        /// </summary>
        [ForeignKey(nameof(Role))]
        public RolesAuthorize RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Список заметок
        /// </summary>
        public virtual List<Note> Notes { get; set; }


        /// <summary>
        /// Создаёт экземпляр класса User 
        /// </summary>
        /// <param name="roleId">Id роли</param>
        public User()
        {
            UserId = Guid.NewGuid();

            PasswordSalt = GetPassSalt();
        }

        /// <summary>
        /// Создаёт экземпляр класса User 
        /// </summary>
        /// <param name="roleId">Id роли</param>
        public User(int roleId)
        {
            UserId = Guid.NewGuid();
            RoleId = RoleId;
            PasswordSalt = GetPassSalt();
        }


        private string GetPassSalt()
        {
            string str = "qwertyuiopasdfghjklzxvcbnm1234567890";
            Random rand = new Random();
            string result = "";
            for (int i = 0; i < 8; i++)
            {
                result += str[rand.Next(0, str.Length - 1)];
            }
            return result;
        }
    }
}
