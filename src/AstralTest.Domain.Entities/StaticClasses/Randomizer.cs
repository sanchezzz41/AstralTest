using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstralTest.Domain.Entities.StaticClasses
{
    /// <summary>
    /// Статический класс с методами для Domain.Entites
    /// </summary>
    public static class Randomizer
    {
        /// <summary>
        /// Возвращает рандомную строку
        /// </summary>
        /// <param name="lenght">Длина возвращаемой строки</param>
        /// <returns></returns>
        public static string GetString(int lenght)
        {
            StringBuilder result = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < lenght; i++)
            {
                result.Append((char)rand.Next('a', 'z'));
            }
            return result.ToString();
        }
    }
}
