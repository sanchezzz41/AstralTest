using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Domain.ContextDb
{
    /// <summary>
    ///  Класс для добавления серсисов
    /// </summary>
    public static class DomainServices
    {

        /// <summary>
        /// Добавляет сервисы из Domain в IServiceCollection 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<INoteService, NoteService>();
            service.AddScoped<IEmailSender, EmailSenderService>();
            return service;
        }
    }
}
