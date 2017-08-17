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
    public class InfoEnteredUserService : IInfoEnteredUserService
    {
        /// <summary>
        /// Содержит информацию о том, к чему обращались пользователи
        /// </summary>
        public List<InfoAboutAction> InfoUsers
        {
            get
            {
                return _context.InfoAboutAction
                    .Include(x => x.EnteredUser)
                    .ToList();
            }
        }

        private readonly DatabaseContext _context;
        private readonly IEnteredUserService _enteredService;

        public InfoEnteredUserService(DatabaseContext context, IEnteredUserService enteredService)
        {
            _context = context;
            _enteredService = enteredService;
        }

        /// <summary>
        /// Добавляет информацию о том, к чему обращается пользователь
        /// </summary>
        /// <param name="infoModel"></param>
        /// <param name="idEntUser">Id обращаегося пользователя</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(InfoEntUserModel infoModel, Guid idEntUser)
        {
            var resultEntUser = await _context.ActionsLogs.SingleOrDefaultAsync(x => x.Id == idEntUser);

            if (resultEntUser == null)
            {
                throw new NullReferenceException(
                    $"Пользователя, который обращается к приложению с таким Id{idEntUser} не существует.");
            }
            var result = new InfoAboutAction(idEntUser, infoModel.NameController, infoModel.NameAction,
                infoModel.Parametrs);
            await _context.InfoAboutAction.AddAsync(result);
            await _context.SaveChangesAsync();
            return result.Id;
        }

        /// <summary>
        /// Удаляет информацию
        /// </summary>
        /// <param name="idInfo"></param>
        /// <returns></returns>
        public async Task Delete(Guid idInfo)
        {
            var resultInfo = await _context.InfoAboutAction.SingleOrDefaultAsync(x => x.Id == idInfo);
            if (resultInfo == null)
            {
                throw new NullReferenceException($"Информации с таким Id{idInfo} не существует.");
            }
            _context.InfoAboutAction.Remove(resultInfo);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает всю информацию о том, кто и когда обращался к приложению
        /// </summary>
        /// <returns></returns>
        public async Task<List<InfoAboutAction>> GetAsync()
        {
            return await _context.InfoAboutAction
                .Include(x => x.EnteredUser)
                .ToListAsync();
        }
    }
}

