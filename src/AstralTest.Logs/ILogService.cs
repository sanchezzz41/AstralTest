using System.Collections.Generic;
using System.Threading.Tasks;

namespace AstralTest.Logs
{
    /// <summary>
    /// Интерфейс для созд. логов
    /// </summary>
    /// <typeparam name="T">Параметр, на основе которого будут создавать логи</typeparam>
    public interface ILogService<T>
    {
        /// <summary>
        /// Создаёт логи на основе модели, которая включает название контроллера, действия, имя пользоватея
        /// и параметры
        /// </summary>
        /// <param name="logModel"></param>
        /// <returns></returns>
        Task Log(T logModel);

        /// <summary>
        /// Конверитирует логов в пдф
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        Task<byte[]> ConvertToPdf(IEnumerable<T> List);
    }
}
