using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс для работы с контейнером задач
    /// </summary>
    public class TasksContainerService : ITasksContainerService
    {
        private readonly DatabaseContext _context;

        public TasksContainerService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Контейнеры для задач
        /// </summary>
        public IEnumerable<TasksContainer> TasksContainers
        {
            get { return _context.TasksContainers
                    .Include(x => x.Master)
                    .Include(x => x.Tasks)
                    .ToList(); }
        }

        /// <summary>
        /// Добавления контейнер для задач
        /// </summary>
        /// <param name="idMaster">Id пользователя, которому будет добавляться контейнер</param>
        /// <param name="containerModel">Модель для добавления контейнера</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(Guid idMaster, TasksContainerModel containerModel)
        {
            if (containerModel == null)
            {
                throw new NullReferenceException();
            }
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == idMaster);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            var resultContainer = new TasksContainer(idMaster, containerModel.Name);
            await _context.TasksContainers.AddAsync(resultContainer);

            await _context.SaveChangesAsync();

            return resultContainer.ListId;
        }

        /// <summary>
        /// Изменяет контейнер для задач
        /// </summary>
        /// <param name="idContainer">Id контейнера, который надо изменить</param>
        /// <param name="containerModel">Модель для изменения контейнера</param>
        /// <returns></returns>
        public async Task EditAsyc(Guid idContainer, TasksContainerModel containerModel)
        {

            if (containerModel == null)
            {
                throw new NullReferenceException();
            }
            var result = await _context.TasksContainers.SingleOrDefaultAsync(x => x.ListId == idContainer);
            if (result == null)
            {
                throw new NullReferenceException();
            }
            result.Name = containerModel.Name;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет контейнер
        /// </summary>
        /// <param name="idContainer">Id контейнера, который надо удалить</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idContainer)
        {
            var result = await _context.TasksContainers.SingleOrDefaultAsync(x => x.ListId == idContainer);
            if (result != null)
            {
                _context.TasksContainers.Remove(result);

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Возвращает список контейнеров
        /// </summary>
        /// <returns></returns>
        public async Task<List<TasksContainer>> GetAsync()
        {
            return await _context.TasksContainers
                .Include(x => x.Master)
                .Include(x => x.Tasks)
                .ToListAsync();
        }
    }
}
