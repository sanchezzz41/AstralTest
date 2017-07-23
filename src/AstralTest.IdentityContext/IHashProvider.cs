using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.IdentityContext
{
    /// <summary>
    /// Интерфейс для хэширования пароля методом MD5
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        /// Возвращает хэш пароля методом MD5
        /// </summary>
        /// <param name="Pass"></param>
        /// <returns></returns>
        string GetHash(string Pass);
    }
}
