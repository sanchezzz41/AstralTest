using System;
using System.Collections.Generic;
using System.Text;

namespace AstralTest.Identity.JWTModel
{
    /// <summary>
    /// Класс содержащий информацию о токене
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Предоставляет токен
        /// </summary>
        public string TokenContent { get; set; }
        /// <summary>
        /// Предоставляет имя пользователя, который владаеет токеном
        /// </summary>
        public string UserName { get; set; }
    }
}
