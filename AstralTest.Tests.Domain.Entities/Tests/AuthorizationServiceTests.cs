using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
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

        //Кэш где хранятся данные при отправке смс
        private IMemoryCache _memoryCache;

        //Импровезированое хранилище
        private List<User> _users;

        //Код который отправляют 
        private string _codeSms;


        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            _memoryCache = TestInitializer.Provider.GetRequiredService<IMemoryCache>();
            _users = await _context.Users.ToListAsync();

            //Services
            _service = new AuthorizationService(GetUserService(), GetPasswordHasher(), _memoryCache, GetSms());
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
        }


        //Возвращает PassworhHasher 
        public ISmsService GetSms()
        {
            var result = new Mock<ISmsService>();

            result.Setup(x => x.SendSmsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>(async (a, b) =>
                {
                    _codeSms = b;
                    await Task.FromResult(_codeSms);
                });

            return result.Object;
        }

        public IUserService GetUserService()
        {
            var result = new Mock<IUserService>();

            //Возвращение всех пользователей
            result.Setup(x => x.GetAsync())
                .Returns(() => Task.FromResult(_users));

            //Добавления пользователя
            result.Setup(x => x.AddAsync(It.IsAny<UserRegisterModel>()))
                .Returns<UserRegisterModel>(async a =>
                {
                    var user = new User(a.UserName, a.Email, a.PhoneNumber, Randomizer.GetString(5), a.Password,
                        a.RoleId);
                    _users.Add(user);
                    return await Task.FromResult(user.UserId);
                });
            //Возвращение всех пользователей
            result.Setup(x => x.Users)
                .Returns(() => _users);



            //Востановление пароля
            result.Setup(x => x.ResetPassword(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns<Guid, string>(async (a, b) =>
                {
                    await Task.Run(() =>
                    {
                        var user = _users.SingleOrDefault(x => x.UserId == a);
                        if (user != null)
                        {
                            user.PasswordHash = b;
                        }
                    });
                });

            return result.Object;
        }


        public IPasswordHasher<User> GetPasswordHasher()
        {
            var result = new Mock<IPasswordHasher<User>>();

            result.Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns<User, string>((a, b) => b);

            return result.Object;
        }



        /// <summary>
        /// Тест на авторизацию пользователя(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_Success()
        {
            //Пароль "admin"
            var userForIdentity = await _context.Users.SingleOrDefaultAsync(x => x.UserName == "admin");

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
            var userForIdentity = await _context.Users.SingleOrDefaultAsync(x => x.UserName == "admin");

            //act
            var resultUser = _service.Authorization(userForIdentity.UserName, "PasswordWTF");

            //assert
            Assert.IsNull(resultUser);
        }

        /// <summary>
        /// Тест на отпарвку смс  пользователю(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_SmsSent_Success()
        {

            //act
            var resultId = await _service.SendSmsToResetPassword("admin");
            var resultObj = _memoryCache.Get<ResetPasswordModel>(resultId);

            //assert
            Assert.NotNull(resultObj);
            Assert.AreEqual(_codeSms, resultObj.Code);
        }

        /// <summary>
        /// Тест на проверку смс кода(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_ConfirmSmsCode_Success()
        {
            var smsCode = "12345";
            var user = await _context.Users.SingleAsync(x => x.UserName == "admin");
            var resetModel=new ResetPasswordModel{Code = smsCode,IdUser = user.UserId};
            var idAction = Guid.NewGuid();
            _memoryCache.Set(idAction, resetModel);

            //act
            var resultId = await _service.ConfirmCodeFromSms(idAction, smsCode);
            var resultUserId = _memoryCache.Get<Guid>(resultId);

            //assert
            Assert.AreEqual(user.UserId, resultUserId);
        }

        /// <summary>
        /// Тест на проверку востановление пароля(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_ResetPassword_Success()
        {
            var newPass = "newpass";
            var user = await _context.Users.SingleAsync(x => x.UserName == "admin");
            var idAction = Guid.NewGuid();
            _memoryCache.Set(idAction, user.UserId);

            //act
            await _service.ResetPassword(idAction, newPass);

            //assert
            Assert.AreEqual(user.PasswordHash, newPass);
        }



    }
}
