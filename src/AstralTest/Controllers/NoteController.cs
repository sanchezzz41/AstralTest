using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Entities;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    //Контроллер для работы с заметками
    [Route("Note")]
    public class NoteController : Controller
    {
        private readonly INoteService _context;
        public NoteController(INoteService context)
        {
            _context = context;
        }

        //Возвращает все заметки
        [HttpGet]
        public async Task<List<Note>> List()
        {
            return await _context.GetAsync();
        }

        //Возвращает заметки в опр. интервале
        [HttpGet("List")]
        public async Task<List<Note>> List(int minVal, int maxVal)
        {
            if (minVal > 0 && minVal < maxVal)
            {
                var prom = await _context.GetAsync();
                var result = prom.OrderBy(x => x.Master.Name).Skip(minVal).Take(maxVal - minVal).ToList();
                return result;
            }
            return null;

        }

        //Добавляет заметку
        [HttpPost]
        public async Task<Guid> AddNote([FromBody]NoteModel mod)
        {
            var resultId = await _context.AddAsync(mod);
            return resultId;
        }

        //Удаляет запись по id
        [HttpDelete("{id}")]
        public async Task DeleteNote(Guid id)
        {
            await _context.DeleteAsync(id);
        }



        //Изменяет запись
        [HttpPut]
        public async Task EditNote([FromBody]NoteModel mod)
        {
            await _context.EditAsync(mod);

        }
    }
}
