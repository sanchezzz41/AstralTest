using System;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Services;
using AstralTest.Identity;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

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

            //Database and cache
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase());

            //Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IEmailSender, EmailSenderService>();
            services.AddScoped<IUserTaskService, UserTaskService>();
            services.AddScoped<ITasksContainerService, TasksContainerService>();
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();

            //Factories
            services.AddScoped<UserDataFactory>();
            services.AddScoped<NoteDataFactory>();
            services.AddScoped<TasksContainerDataFactory>();
            services.AddScoped<UserTaskDataFactory>();

            Provider = services.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public void DownUpConfig()
        {
            Provider.GetService<DatabaseContext>().Database.EnsureDeleted();
        }
    }
}
