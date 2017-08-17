using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий информацию о том, к чему обращается пользователь
    /// </summary>
    public class InfoAboutAction
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }


        /// <summary>
        /// Время обращения пользователя к приложению
        /// </summary>
        [Required]
        public DateTime EnteredTime { get; set; }

        /// <summary>
        /// Параметры для передачи методу в контроллере
        /// </summary>
        public string JsonParametrs { get; set; }

        /// <summary>
        /// Внешний ключ который ссылается на пользователя, который обращается к приложению
        /// </summary>
        [ForeignKey(nameof(EnteredUser))]
        public Guid IdEnteredUser { get; set; }


        public ActionLog EnteredUser { get; set; }

        public InfoAboutAction()
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
        public InfoAboutAction(Guid idEntereUser,string nameController, string nameAction, string paremetrsAction)
        {
            Id = Guid.NewGuid();
            EnteredTime = DateTime.Now;
            IdEnteredUser = idEntereUser;
            JsonParametrs = paremetrsAction;
        }
    }
}
