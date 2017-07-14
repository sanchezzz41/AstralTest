using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AstralTest.DataDb;

namespace AstralTest.ContextDb
{
    public class AstralContext:DbContext
    {
        public AstralContext(DbContextOptions<AstralContext> opt):base(opt)
        {

        }
    

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Note>().ToTable("notes");
            modelBuilder.Entity<Note>()
                .HasOne(x => x.Master)
                .WithMany(x => x.Notes)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
        }
    }
}
