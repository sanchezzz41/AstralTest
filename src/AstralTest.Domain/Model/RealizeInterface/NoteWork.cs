using AstralTest.Domain.Context;
using AstralTest.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Model.RealizeInterface
{
    public class NoteWork : INote
    {
        private AstralContext _context { get; }

        public NoteWork(AstralContext context)
        {
            _context = context;
        }

        public IEnumerable<Note> Notes
        {
            get
            {
               return _context.Notes.Include(x=>x.Master).ToList();
            }
        }

        public void AddNote(User user, Note note)
        {
            if (user != null 
                && note != null)
            {
                var resUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (resUser != null)
                {
                    //1й вариант
                    note.Master = resUser;
                    note.MasterId = resUser.Id;
                    _context.Notes.Add(note);

                    //2й вариант - Не работает(потом разобраться)
                    //resUser.Notes.Add(note);
                    //_context.Users.Attach(resUser);
                    //_context.Entry(resUser).Property(x => x.Notes).IsModified = true;


                    _context.SaveChanges();
                }
            }
            else
            {
                throw new Exception("NullException");
            }
        }

        public void DeleteNote(Note note)
        {
            if (note != null)
            {
                var result = _context.Notes.FirstOrDefault(x => x.Id == note.Id);
                if (result != null)
                {
                    _context.Notes.Remove(result);
                   _context.SaveChanges();
                }
            }
            else
            {
                throw new Exception("NullException");
            }
        }

        public void EditNote(Note note)
        {
            if (note != null)
            {
                var result = _context.Notes.FirstOrDefault(x => x.Id == note.Id);
                if (result != null)
                {
                    result.Text = note.Text;
                    _context.Notes.Attach(result);
                    _context.Entry(result).Property(x => x.Text).IsModified = true;
                    _context.SaveChanges();
                }
            }
            else
            {
                throw new Exception("NullException");
            }
        }
    }
}
