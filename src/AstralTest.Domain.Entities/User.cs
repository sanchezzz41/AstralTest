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
    public class User:IdentityUser
    {
        /// <summary>
        /// Заметки пользователя
        /// </summary>
        public virtual List<Note> Notes { get; set; }

        #region Optinal
        ///// <summary>
        ///// Id пользователя
        ///// </summary>
        //[Key]
        //[Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public override Guid Id { get; set; }

        ///// <summary>
        ///// Имя пользователя
        ///// </summary>
        //[Required]
        //public override string UserName { get; set; }

        ///// <summary>
        ///// Иницилизирует класс для пользоваетля(Id автоматически создаётся)
        ///// </summary>
        //public User():base()
        //{
        //    //Id = Guid.NewGuid();
        //}

        ///// <summary>
        ///// Иницилизирует класс для пользоваетля (id создаётся автоматически)
        ///// </summary>
        ///// <param name="name">Имя пользователя</param>
        //public User(string name):base()
        //{
        //    //Id = Guid.NewGuid();
        //    UserName = name;
        //}
        #endregion
    }
}
