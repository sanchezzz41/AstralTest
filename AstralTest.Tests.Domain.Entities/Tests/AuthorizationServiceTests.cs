using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Services;
using AstralTest.Sms;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Randomizer = AstralTest.Domain.Utilits.Randomizer;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Класс для тестирования авторизации
    /// </summary>
    [TestFixture]
    public class AuthorizationServiceTests
    {

        //Тестируемый сервис
        private IAuthorizationService _service;

        //Контекст бд
        private DatabaseContext _context;

        private List<User> _users;

        private string _codeSms;



        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            _users = await _context.Users.ToListAsync();

            var passwordHasher = GetPasswordHasher();
            var userService = new UserService(_context, passwordHasher);
            var memoryService = TestInitializer.Provider.GetService<IMemoryCache>();
            var smsService = GetSms();

            //Services
            _service = new AuthorizationService(userService, passwordHasher, memoryService, smsService);
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
        }

        //Возвращает PassworhHasher 
        public ISmsService GetSms()
        {
            Mock<ISmsService> result = new Mock<ISmsService>();

            result.Setup(x => x.SendSmsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>(async (a, b) =>
                {
                    _codeSms = b;
                    await Task.FromResult(_codeSms);
                });

            return result.Object;
        }


        public IPasswordHasher<User> GetPasswordHasher()
        {
            Mock<IPasswordHasher<User>> result = new Mock<IPasswordHasher<User>>();

            result.Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns<User, string>((a, b) => (a.PasswordSalt + b));

            return result.Object;
        }

        /// <summary>
        /// Тест на авторизацию пользователя(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Category", "UI")]
        public async Task Authorization_User_Success()
        {
            //Пароль "admin"
            var userForIdentity = await _context.Users.SingleOrDefaultAsync(x => x.UserName == "testUser1");

            //act
            var resultUser = _service.Authorization(userForIdentity.UserName, "admin");

            //assert
            Assert.IsNotNull(resultUser);
        }

        /// <summary>
        /// Тест на авторизацию пользователя(ожидается неудача, так как входим за несуществующего пользователя)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_Login_Fail()
        {
            //act
            var resultUser = _service.Authorization("Noname", "admin");

            //assert
            Assert.IsNull(resultUser);
        }

        /// <summary>
        /// Тест на авторизацию пользователя(ожидается неудача, вводим неправильный пароль)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_Password_Fail()
        {
            //Пароль "admin"
            var userForIdentity = await _context.Users.SingleOrDefaultAsync(x => x.UserName == "testUser1");

            //act
            var resultUser = _service.Authorization(userForIdentity.UserName, "PasswordWTF");

            //assert
            Assert.IsNull(resultUser);
        }
    }
}
