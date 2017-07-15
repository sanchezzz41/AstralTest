using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Model
{
    /// <summary>
    /// Класс для изменения пользователя в контролере
    /// </summary>
    public class UserEditModel
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid IdUser { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
    }
}
