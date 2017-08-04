using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Services;
using AstralTest.FileStore;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Класс для проверки сервсиса для работы с задачами 
    /// </summary>
    [TestFixture]
    public class FileServiceTests
    {
        private DatabaseContext _context;

        //Тестируемый сервис
        private IFileService _service;

        //Для проверки значений
        private List<AstralFile> _files;

        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            await TestInitializer.Provider.GetService<TasksContainerDataFactory>().CreateTasksContainer();
            await TestInitializer.Provider.GetService<UserTaskDataFactory>().CreateTasks();
            await TestInitializer.Provider.GetService<FileDataFactory>().CreateFiles();
            _files = await _context.Files.ToListAsync();

            //Services
            _service = new FileService(_context, TestMock());
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<TasksContainerDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<UserTaskDataFactory>().Dispose();
        }

        public IFileStore TestMock()
        {
            Mock<IFileStore> res = new Mock<IFileStore>();
            res.Setup(x => x.Create(It.IsAny<Stream>(), It.IsAny<string>()));
            res.Setup(x => x.Upload(It.IsAny<string>())).Returns<string>(async x => new byte[] {0, 1, 2});

            return res.Object;
        }

        /// <summary>
        /// Тест на загрузку файла из хранилища(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UploadFile_Success()
        {
            var expect = _files.First();
            var file = await _context.Files.SingleOrDefaultAsync(x => x.FileId == expect.FileId);
            //Act
            var result = await _service.GetFileAsync(file.FileId);

            //Assert
            Assert.AreEqual(expect.NameFile, result.NameFile);
            Assert.AreEqual(expect.TypeFile, result.TypeFile);
            CollectionAssert.AreEqual(new byte[] {0, 1, 2}, result.Bytes);
        }
    }
}
    
