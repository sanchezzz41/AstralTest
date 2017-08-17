using Microsoft.Extensions.DependencyInjection;
using AstralTest.Sms.Stub;

namespace AstralTest.Sms
{
    /// <summary>
    /// Класс для добалвения смс сервиса
    /// </summary>
    public static class ExtensionService
    {
        /// <summary>
        /// Метод расширения для добавления смс сервиса
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddSmsService(this IServiceCollection service)
        {
            //Можно потом ещё сделать так, что бы сюда параметры передовалиьс
            service.AddScoped<ISmsService, SmsService>();
            return service;
        }

    }
}
