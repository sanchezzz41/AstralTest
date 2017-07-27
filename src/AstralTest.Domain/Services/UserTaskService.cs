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
    /// Класс для работы с задачами
    /// </summary>
    public class UserTaskService : IUserTaskService
    {
        private readonly DatabaseContext _context;

        public UserTaskService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Список задач
        /// </summary>
        public IEnumerable<UserTask> Tasks
        {
            get
            {
                return _context.Tasks
                    .Include(x => x.MasterList)
                    .Include(x => x.MasterList.Master)
                    .ToList();
            }
        }

        /// <summary>
        /// Добавляет задачу в список
        /// </summary>
        /// <param name="idContainer"></param>
        /// <param name="task">Модель задачи</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(Guid idContainer, UserTaskModel task)
        {
            if (task == null)
            {
                throw new NullReferenceException();
            }
            var taskContainer = await _context.TasksContainers.SingleOrDefaultAsync(x => x.ListId == idContainer);
            if (taskContainer == null)
            {
                throw new NullReferenceException();
            }
            var resultTask = new UserTask(taskContainer.ListId, task.TextTask, task.EndTime);
            await _context.Tasks.AddAsync(resultTask);

            await _context.SaveChangesAsync();

            return resultTask.TaskId;
        }

        /// <summary>
        /// Изменяет задачу
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task EditAsync(Guid idTask, UserTaskModel task)
        {
            if (task == null)
            {
                throw new NullReferenceException();
            }
            var result = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == idTask);
            if (result == null)
            {
                throw new NullReferenceException();
            }

            if (string.IsNullOrEmpty(task.TextTask))
            {
                result.TextTask = task.TextTask;
            }

            result.EndTime = task.EndTime;

            result.ActualTimeEnd = task.ActualTimeEnd.Value;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет задачу
        /// </summary>
        /// <param name="idTask"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idTask)
        {
            var result = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == idTask);
            if (result != null)
            {
                _context.Tasks.Remove(result);

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Возвращает все задачи
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserTask>> GetAsync()
        {
            return await _context.Tasks
                .Include(x => x.MasterList)
                .Include(x => x.MasterList.Master)
                .ToListAsync();
        }
    }
}
