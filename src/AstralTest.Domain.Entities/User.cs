using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AstralTest.Domain.Entities.StaticClasses;

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
        public RolesOption RoleId { get; set; }

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
        public User()
        {
            UserId = Guid.NewGuid();
            PasswordSalt = Randomizer.GetString(8);
        }

        /// <summary>
        /// Создаёт экземпляр класса User 
        /// </summary>
        /// <param name="roleId">Id роли</param>
        public User(int roleId)
        {
            UserId = Guid.NewGuid();
            RoleId = RoleId;
            PasswordSalt = Randomizer.GetString(8);
        }

    }
}
