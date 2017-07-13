using AstralTest.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Interface
{
    public interface INote
    {
        IEnumerable<Note> Notes { get; }
        void AddNote(User user,Note note);
        void DeleteNote(Note note);
        void EditNote(Note note);
    }
}
