using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Model
{
    public class NoteEditModel
    {
        [Required]
        public Guid idNote { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
