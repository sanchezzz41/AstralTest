using System;
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
    [Authorize(Roles =nameof(RolesOption.User))]
    public class NotesController : Controller
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService context)
        {
            _noteService = context;
        }

        //Возвращает все заметки для определенного пользователя
        [HttpGet]
        public async Task<object> List(Guid idMaster)
        {
            var result = await _noteService.GetAsync();
            return result.Where(x=>x.IdUser==idMaster).NotesView();
        }

        //Возвращает заметки в опр. интервале
        [HttpGet("List")]
        public async Task<object> List(int offSet, int count)
        {
            var prom = await _noteService.GetAsync();
            var result = prom.OrderBy(x => x.Master.UserName).Skip(offSet).Take(count).ToList();
            return result.NotesView();
        }

        //Добавляет заметку
        [HttpPost("{idMaster}")]
        public async Task<Guid> AddNote([FromBody] NoteModel mod, Guid idMaster)
        {
            var resultId = await _noteService.AddAsync(mod, idMaster);
            return resultId;
        }

        //Удаляет запись по id
        [HttpDelete("{id}")]
        public async Task DeleteNote(Guid id)
        {
            await _noteService.DeleteAsync(id);
        }

        //Изменяет запись
        [HttpPut("{id}")]
        public async Task EditNote([FromBody]NoteModel mod, Guid id)
        {
            await _noteService.EditAsync(mod, id);

        }
    }
}
