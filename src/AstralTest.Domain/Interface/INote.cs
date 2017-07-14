using AstralTest.DataDb;
using System.Collections.Generic;

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
