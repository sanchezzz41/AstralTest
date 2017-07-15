using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Interface;
using AstralTest.DataDb;
using AstralTest.Domain.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    //Контроллер для работы с заметками
    [Route("Note")]
    public class NoteController : Controller
    {
        private readonly INote _context;
        public NoteController(INote context)
        {
            _context = context;
        }

        //Возвращает все заметки
        [HttpGet("")]
        public async Task<List<Note>> List()
        {
            return await _context.GetAsync();
        }

        //Возвращает заметки в опр. интервале
        [HttpGet("List")]
        public async Task<List<Note>> List(int minVal,int maxVal)
        {
            if (minVal>0 && minVal < maxVal)
            {
                var prom = await _context.GetAsync();
                var result = prom.OrderBy(x => x.Master.Name).Skip(minVal).Take(maxVal - minVal).ToList();
                return result;
            }
            return null; 
          
        }

        //Добавляет заметку
        [HttpPost("")]
        public async Task<Guid> AddNote([FromBody]NoteModel mod)
        {
          var resultId = await _context.AddAsync(new User { Id = mod.IdMaster },
                new Note {Text=mod.Text,MasterId=mod.IdMaster });
            return resultId;
        }

        //Удаляет запись по id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var res = _context.Notes.SingleOrDefault(x => x.Id == id);
            if(res==null)
            {
                return NotFound();
            }
            await _context.DeleteAsync(res);
            return View();
        }


        //Изменяет запись
        [HttpPut("")]
        public async Task<IActionResult> EditNote([FromBody]NoteEditModel mod)
        {
            var result = new Note { Id = mod.idNote, Text = mod.Text };
            await _context.EditAsync(result);
            return View();
        }
    }
}
