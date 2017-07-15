using AstralTest.Domain.Interface;
using AstralTest.Domain.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Domain.ContextDb
{
    /// <summary>
    ///  Класс для добавления серсисов
    /// </summary>
    public static class MyServices
    {

        /// <summary>
        /// Добавляет сервисы в IServiceCollection 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyServices(this IServiceCollection service)
        {
            service.AddScoped<IUser, UserService>();
            service.AddScoped<INote, NoteService>();
            return service;
        }
    }
}
