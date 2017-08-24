using System;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Identity.JWTAuthorization
{
    /// <summary>
    /// Класс добавляющий сервис для работы с JWT
    /// </summary>
    public static class JwtServices
    {
        /// <summary>
        /// Метод расширения, который добавляет JWT сервис в IServiceCollection
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configure">Конфигурации для FileStore</param>
        /// <returns></returns>
        public static IServiceCollection AddJwtService(this IServiceCollection service,
            Action<TokenOption> configure)
        {
            service.AddScoped<IJwtService, JwtService>();

            var tokenOption = new TokenOption();
            configure(tokenOption);
            service.AddSingleton(tokenOption);

            return service;
        }
    }
}
