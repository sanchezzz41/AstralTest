using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий пользователя, который обращаются к приложению
    /// </summary>
    public class EnteredUser
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }


        /// <summary>
        /// Внеший ключ указывающий на пользователя
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid IdUser { get; set; }

        /// <summary>
        /// Пользователь, который обращается к приложению
        /// </summary>
        [Required]
        public User User { get; set; }

        public List<InfoAboutEnteredUser> InfoAboutEnteredUsers { get; set; }

        public EnteredUser()
        {
            Id = Guid.NewGuid();
        }

        public EnteredUser(Guid idUser)
        {
            Id = Guid.NewGuid();
            IdUser = idUser;
        }
    }
}
