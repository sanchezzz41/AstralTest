using AstralTest.ContextDb;
using AstralTest.DataDb;
using AstralTest.Domain.Interface;
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
    public class NoteService : INote
    {
        private AstralContext _context { get; }

        public NoteService(AstralContext context)
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
        /// <param name="user">Владелек заметки</param>
        /// <param name="note">Заетка для добавления в бд</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(User user, Note note)
        {
            if (user == null
                && note == null)
            {
                throw new Exception("NullException");
            }
            var resUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == user.Id);
            if (resUser == null)
            {
                throw new Exception("NullException");
            }
            note.Master = resUser;
            note.MasterId = resUser.Id;
            await _context.Notes.AddAsync(note);

            await _context.SaveChangesAsync();
            return note.Id;
        }

        /// <summary>
        /// Удаляет заметку из бд
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Note note)
        {
            if (note == null)
            {
                throw new Exception("NullException");

            }
            var result = await _context.Notes.SingleOrDefaultAsync(x => x.Id == note.Id);
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
        public async Task EditAsync(Note note)
        {
            if (note == null)
            {
                throw new Exception("NullException");
            }

            var result = await _context.Notes.SingleOrDefaultAsync(x => x.Id == note.Id);
            if (result == null)
            {
                throw new Exception("NullException");
            }
            result.Text = note.Text;
            _context.Notes.Attach(result);
            _context.Entry(result).Property(x => x.Text).IsModified = true;
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
