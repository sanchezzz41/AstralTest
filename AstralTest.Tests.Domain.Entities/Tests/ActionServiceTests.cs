using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Services;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Класс для тестирования action's
    /// </summary>
    [TestFixture]
    public class ActionServiceTests
    {

        //Тестируемый сервис
        private IActionService _service;

        //Контекст бд
        private DatabaseContext _context;

        //Хранилще для проверки
        private List<ActionLog> _actionLogs;


        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            await TestInitializer.Provider.GetService<ActionDataFactory>().CreateActions();
            _actionLogs = await _context.ActionsLogs.ToListAsync();

            //Services
            _service = new ActionService(_context);
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<ActionDataFactory>().Dispose();
        }

        /// <summary>
        ///  Добавляет информацию о входящем пользователе(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddAction_Success()
        {
            var userName = "admin";
            var controllername = "testContoller";
            var actionName = "tetsAction";
            //act
            var resultId = await _service.AddAsync(userName, controllername, actionName);
            var resultAction = await _context.ActionsLogs.SingleAsync(x => x.Id == resultId);
            //assert
            Assert.AreEqual(userName, resultAction.User.UserName);
            Assert.AreEqual(controllername, resultAction.NameOfController);
            Assert.AreEqual(actionName, resultAction.NameOfAction);
        }

        /// <summary>
        ///  Возвращение всей информации о входящих пользователях(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetActions_Success()
        {
            //act
            var resultList= await _service.GetAsync();
            //assert
            CollectionAssert.AreEqual(_actionLogs, resultList);
        }
    }
}
