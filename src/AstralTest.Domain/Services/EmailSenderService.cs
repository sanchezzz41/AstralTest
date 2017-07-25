using AstralTest.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AstralTest.Domain.Service
{
    /// <summary>
    /// Заглушка для отправки сообщений
    /// </summary>
    public class EmailSenderService : IEmailSender
    {
        private ILogger _logs;
        public int MyProperty { get; set; }
        public EmailSenderService(ILogger<EmailSenderService> log)
        {
            _logs = log;
        }
        public async Task SendEmail(string email, string name, string text)
        {
            _logs.LogInformation(2, $"Сообщение на адрес {email}(получатель:{name}) с текстом {text} отправленно!");
        }
    }
}
