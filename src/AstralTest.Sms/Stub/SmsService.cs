using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AstralTest.Sms.Stub
{
    /// <summary>
    /// Класс реализующий интефрфейс для отправки smc
    /// </summary>
    public class SmsService : ISmsService
    {
        private readonly ILogger _logs;

        public SmsService(ILogger<SmsService> log)
        {
            _logs = log;
        }

        /// <summary>
        /// Отправляет смс на данный номер
        /// </summary>
        /// <param name="phoneNumber">Номер, на который надо отправить текст</param>
        /// <param name="text">Текст для отправки</param>
        /// <returns></returns>
        public async Task SendSmsAsync(string phoneNumber, string text)
        {
            await Task.Run(() => _logs.LogInformation(2,
                $"Сообщение на телефон {phoneNumber} с текстом {text} отправленно!"));
        }
    }
}
