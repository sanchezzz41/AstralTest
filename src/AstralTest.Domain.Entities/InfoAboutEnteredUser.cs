using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий информацию о том, к чему обращается пользователь
    /// </summary>
    public class InfoAboutEnteredUser
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Название контроллера к которому общараются
        /// </summary>
        [Required]
        public string NameOfController { get; set; }

        /// <summary>
        /// Название метода действия, к которому обращаются
        /// </summary>
        [Required]
        public string NameOfAction { get; set; }

        /// <summary>
        /// Время обращения пользователя к приложению
        /// </summary>
        [Required]
        public DateTime EnteredTime { get; set; }

        /// <summary>
        /// Параметры для передачи методу в контроллере
        /// </summary>
        public string ParametrsToAction { get; set; }

        /// <summary>
        /// Внешний ключ который ссылается на пользователя, который обращается к приложению
        /// </summary>
        [ForeignKey(nameof(EnteredUser))]
        public Guid IdEnteredUser { get; set; }


        public EnteredUser EnteredUser { get; set; }

        public InfoAboutEnteredUser()
        {
            Id = Guid.NewGuid();
            EnteredTime = DateTime.Now;
        }

        /// <summary>
        /// Иницилизирует новый экземпляр класса
        /// </summary>
        /// <param name="idEntereUser">Id пользователя, который обращается к приложению</param>
        /// <param name="nameController">Название контроллера</param>
        /// <param name="nameAction">Название действия</param>
        /// <param name="paremetrsAction">Параметры передаваемые методу действия</param>
        public InfoAboutEnteredUser(Guid idEntereUser,string nameController, string nameAction, string paremetrsAction)
        {
            Id = Guid.NewGuid();
            EnteredTime = DateTime.Now;
            IdEnteredUser = idEntereUser;
            NameOfController = nameController;
            NameOfAction = nameAction;
            ParametrsToAction = paremetrsAction;
        }
    }
}
