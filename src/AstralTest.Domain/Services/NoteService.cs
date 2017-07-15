using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Service
{

    /// <summary>
    /// Класс для работы с заметками
    /// </summary>
    public class NoteService : INoteService
    {
        private DatabaseContext _context { get; }

        public NoteService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Note> Notes
        {
            get
            {
                return _context.Notes.Include(x => x.Master).ToList();
            }
        }

        /// <summary>
        /// Добавляет заметку в бд
        /// </summary>
        /// <returns></returns>
        public async Task<Guid> AddAsync(NoteModel noteModel)
        {
            if(noteModel==null && noteModel.IdMaster==null)
            {
                throw new Exception("NullException");
            }
            var resUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == noteModel.IdMaster);

            if (resUser == null)
            {
                throw new Exception("NullException");
            }

            var result = new Note(noteModel.Text, noteModel.IdMaster);

            if (_context.Notes.Any(x=>x.Id==result.Id))
            {
                throw new Exception("IdExists");
            }
            //note.Master = resUser;
            //note.MasterId = resUser.Id;
            await _context.Notes.AddAsync(result);
            await _context.SaveChangesAsync();
            return result.Id;
        }

        /// <summary>
        /// Удаляет заметку из бд
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            if (id == null)
            {
                throw new Exception("IdEqualNullException");
            }

            var result = await _context.Notes.SingleOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                throw new Exception("NullException");
            }
            _context.Notes.Remove(result);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Изменяет заметку
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task EditAsync(NoteModel newNote)
        {
            if(newNote!=null && newNote.Id==null)
            {
                throw new Exception("NullException");
            }

            var result = await _context.Notes.SingleOrDefaultAsync(x => x.Id == newNote.Id);
            if (result == null)
            {
                throw new Exception("NullException");
            }
           
            result.Text = newNote.Text;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает заметки из БД
        /// </summary>
        /// <returns></returns>
        public async Task<List<Note>> GetAsync()
        {
            var result = await _context.Notes.ToListAsync();
            return result;
        }

    }
}
