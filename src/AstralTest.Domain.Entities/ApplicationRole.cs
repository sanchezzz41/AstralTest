using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Entities
{
    public class ApplicationRole:IdentityRole<Guid>
    {
        [Key]
        public override Guid Id { get; set; }    
    }
}
