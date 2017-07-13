using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Model
{
    public class Note
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("text")]
        public string Text { get; set; }

        [Column("iduser")]
        public Guid? MasterId { get; set; }

       
        public  User Master { get; set; }    
    }
}
