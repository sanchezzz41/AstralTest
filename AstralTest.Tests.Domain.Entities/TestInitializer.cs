using System;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Services;
using AstralTest.FileStore;
using AstralTest.GeoLocation;
using AstralTest.Identity;
using AstralTest.Sms;
using AstralTest.Sms.Stub;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using AstralTest.Logs;

namespace AstralTest.Tests.Domain.Entities
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider Provider { get; private set; }

        [OneTimeSetUp]
        public void SetUpConfig()
        {
            var services = new ServiceCollection();

            //Database 
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase());

            services.AddMemoryCache();

            //Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IEmailSender, EmailSenderService>();
            services.AddScoped<IUserTaskService, UserTaskService>();
            services.AddScoped<ITasksContainerService, TasksContainerService>();
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileStore, FileStore.FileStore>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IGeoService, YandexGeoService>();
            services.AddScoped<ILogService<LogModel>, LogService>();
            services.AddScoped<IAttachmentsService, AttachmentsService>();
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<IInfoActionService, InfoActionService>();


            //Factories
            services.AddScoped<UserDataFactory>();
            services.AddScoped<NoteDataFactory>();
            services.AddScoped<TasksContainerDataFactory>();
            services.AddScoped<UserTaskDataFactory>();
            services.AddScoped<FileDataFactory>();
            services.AddScoped<ActionDataFactory>();
            services.AddScoped<InfoAboutActionDataFactory>();

            Provider = services.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public void DownUpConfig()
        {
            Provider.GetService<DatabaseContext>().Database.EnsureDeleted();
        }
    }
}
