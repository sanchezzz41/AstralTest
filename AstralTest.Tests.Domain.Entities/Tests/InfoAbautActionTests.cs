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
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Тесты для сервиса который работает с параметрами для логов
    /// </summary>
    [TestFixture]
    public class InfoAbautActionTests
    {

        //Тестируемый сервис
        private IInfoActionService _service;

        //Контекст бд
        private DatabaseContext _context;

        //Хранилще для проверки
        private List<ParametrsAction> _infoAboutActions;


        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            await TestInitializer.Provider.GetService<ActionDataFactory>().CreateActions();
            await TestInitializer.Provider.GetService<InfoAboutActionDataFactory>().CreateInfoActions();
            _infoAboutActions = await _context.ParametrsActions.ToListAsync();

            //Services
            _service = new InfoActionService(_context);
        }


        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<ActionDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<InfoAboutActionDataFactory>().Dispose();
        }

        /// <summary>
        /// Добавление информации в действие(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Add_ActionInfo_Success()
        {
            var param = "params";
            var resultActin = await _context.ActionsLogs.FirstAsync();
            //act
            var resultId = await _service.AddAsync(param, resultActin.Id);
            var resultInfo = await _context.ParametrsActions.SingleOrDefaultAsync(x => x.Id == resultId);
            //assert
            Assert.AreEqual(param, resultInfo.JsonParametrs);
        }

        /// <summary>
        /// Возвращает все параметры(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Get_ActionInfo_Success()
        {
            //act
            var resultList = await _service.GetAsync();
            //assert
            CollectionAssert.AreEqual(_infoAboutActions, resultList);
        }
    }
}
