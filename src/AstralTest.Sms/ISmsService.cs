using System.Threading.Tasks;

namespace AstralTest.Sms
{
    /// <summary>
    /// Интерфейс для отправки Sms
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// Отправляет смс на данный номер
        /// </summary>
        /// <param name="phoneNumber">Номер, на который надо отправить текст</param>
        /// <param name="text">Текст для отправки</param>
        /// <returns></returns>
        Task SendSmsAsync(string phoneNumber, string text);
    }
}
