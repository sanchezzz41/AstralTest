using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{

    /// <summary>
    /// Предоставляет класс для пользоваетля
    /// </summary>
    public class User
    {
     
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Заметки пользователя
        /// </summary>
        public virtual  ICollection<Note> Notes { get; set; }

        /// <summary>
        /// Иницилизирует класс для пользоваетля(Id автоматически создаётся)
        /// </summary>
        public User()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Иницилизирует класс для пользоваетля (id создаётся автоматически)
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

    }
}
