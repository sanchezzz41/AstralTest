﻿using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Domain
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
            service.AddScoped<IAuthorizationService, AuthorizationService>();
            service.AddScoped<IEmailSender, EmailSenderService>();
            service.AddScoped<IUserTaskService, UserTaskService>();
            service.AddScoped<ITasksContainerService, TasksContainerService>();
            service.AddScoped<IFileService, FileService>();
            return service;
        }
    }
}
