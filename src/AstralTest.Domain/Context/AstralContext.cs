using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.Model;

namespace AstralTest.Domain.Context
{
    public class AstralContext:DbContext
    {
        public AstralContext(DbContextOptions<AstralContext> opt):base(opt)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
