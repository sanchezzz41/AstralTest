using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AstralTest.Database
{
    //public class DatabaseContext:IdentityDbContext<User, ApplicationRole, Guid>
    //{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt):base(opt)
        {
        }
    
        public DbSet<Note> Notes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Ignore<IdentityUserLogin<string>>();
        //    modelBuilder.Ignore<IdentityUserToken<string>>();
        //    modelBuilder.Ignore<IdentityRoleClaim<int>>();
        //    modelBuilder.Ignore<IdentityUserClaim<int>>();

        //    modelBuilder.Entity<IdentityUserRole<string>>()
        //        .HasKey(x => new { x.RoleId, x.UserId });

        //    //Каскадое удаление не заработало, если это закоментить и просто указать внешний ключ
        //    modelBuilder.Entity<Note>()
        //        .HasOne(x => x.Master)
        //        .WithMany(x => x.Notes)
        //        .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);

        //}
    }
}
