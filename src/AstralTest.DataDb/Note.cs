using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.DataDb
{
    public class Note
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; }

        [ForeignKey(nameof(Master))]
        public Guid? MasterId { get; set; }      
       
        public virtual User Master { get; set; }    
    }
}
