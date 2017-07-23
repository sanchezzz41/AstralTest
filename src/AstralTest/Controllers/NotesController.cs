using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Extensions;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    //Контроллер для работы с заметками
    [Route("Note")]
    [Authorize]
    public class NotesController : Controller
    {
        private readonly INoteService _context;
        public NotesController(INoteService context)
        {
            _context = context;
        }

        //Возвращает все заметки
        [HttpGet]
        public async Task<object> List()
        {
            var result = await _context.GetAsync();
            return result.NotesView();
        }

        //Возвращает заметки в опр. интервале
        [HttpGet("List")]
        public async Task<object> List(int offSet, int count)
        {
            var prom = await _context.GetAsync();
            var result = prom.OrderBy(x => x.Master.UserName).Skip(offSet).Take(count).ToList();
            return result.NotesView();
        }

        //Добавляет заметку
        [HttpPost("{idMaster}")]
        public async Task<Guid> AddNote([FromBody] NoteModel mod, Guid idMaster)
        {
            var resultId = await _context.AddAsync(mod, idMaster);
            return resultId;
        }

        //Удаляет запись по id
        [HttpDelete("{id}")]
        public async Task DeleteNote(Guid id)
        {
            await _context.DeleteAsync(id);
        }

        //Изменяет запись
        [HttpPut("{id}")]
        public async Task EditNote([FromBody]NoteModel mod, Guid id)
        {
            await _context.EditAsync(mod, id);

        }
    }
}
