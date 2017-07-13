using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Interface;
using AstralTest.Model;
using AstralTest.Domain.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    [Route("Note")]
    public class NoteController : Controller
    {
        private readonly INote _context;
        public NoteController(INote context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult List()
        {
            return View(_context.Notes.OrderBy(x=>x.Master.Name).ToList());
        }

        [HttpGet("List")]
        public IActionResult List(int minVal,int maxVal)
        {
            if (minVal>0 && minVal < maxVal)
            {
                var result = _context.Notes.OrderBy(x => x.Master.Name).Skip(minVal).Take(maxVal - minVal).ToList();
                return View(result);
            }
            return View();
          
        }

        [HttpPost("AddNote")]
        public IActionResult AddNote([FromBody]NoteModel mod)
        {
            _context.AddNote(new Domain.Model.User { Id = mod.IdMaster },
                new Domain.Model.Note { Id = Guid.NewGuid(), Text = mod.Text });
            return View();
        }

        [HttpDelete("DeleteNote/{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            var res = _context.Notes.FirstOrDefault(x => x.Id == id);
            _context.DeleteNote(res);
            return View();
        }

        [HttpPut("EditNote")]
        public IActionResult EditNote([FromBody]NoteEditModel mod)
        {
            var result = new Note { Id = mod.idNote, Text = mod.Text };
            _context.EditNote(result);
            return View();
        }
    }
}
