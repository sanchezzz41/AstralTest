using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий роль для пользователя
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Id Роли
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public RolesAuthorize RoleId { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string RoleName { get; set; }
    
        /// <summary>
        /// Пользователи, которые имеют эту роль
        /// </summary>
        public virtual List<User> Users { get; set; }

        public Role()
        {

        }
        /// <summary>
        /// Иницилизурет новый экземляр класса Role 
        /// </summary>
        /// <param name="role">Имя роли</param>
        public Role(RolesAuthorize role)
        {

            RoleName = Enum.GetName(typeof(RolesAuthorize), role);
            RoleId = role;
        }
    }

    /// <summary>
    /// Предоставляет перечесление для ролей
    /// </summary>
    public enum RolesAuthorize
    {
        user = 1,
        admin = 2
    }
}
 