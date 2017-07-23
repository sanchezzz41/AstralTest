using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Database
{
    //public class DatabaseContext:IdentityDbContext<User, ApplicationRole, Guid>
    //{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt):base(opt)
        {
        }
    
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(x => x.UserName)
                .IsUnique();
        }

        public async Task Initialize(IServiceProvider serviceProvider)
        {
            //Тут создаются 2 роли(admin и user), если таковых нет
            {
                var roleUser = await Roles.SingleOrDefaultAsync(x => x.RoleName == RolesAuthorize.user.ToString());
                if (roleUser == null)
                {
                    Roles.Add(new Role(RolesAuthorize.user));
                }

                var roleAdmin =await Roles.SingleOrDefaultAsync(x => x.RoleName == RolesAuthorize.admin.ToString());
                if (roleAdmin == null)
                {
                    Roles.Add(new Role(RolesAuthorize.admin));
                }

                if (roleUser == null || roleAdmin == null)
                {
                    await SaveChangesAsync();
                }
            }
             //Тут создаётся пользователь admin, если такового нет
            {
                if(await Users.SingleOrDefaultAsync(x=>x.UserName=="admin")==null)
                {
                    var resRole =await Roles.SingleAsync(x => x.RoleName == RolesAuthorize.admin.ToString());
                    var resultUser = new User { Email = "admin@mail.com", UserName = "admin", RoleId = resRole.RoleId };
                    var hashProvider = serviceProvider.GetRequiredService<IPasswordHasher<User>>();
                    var resultHash = hashProvider.HashPassword(resultUser, "admin");
                    resultUser.PasswordHash = resultHash;
                    Users.Add(resultUser);
                    await SaveChangesAsync();
                }               
            }
            
        }
    }
}
