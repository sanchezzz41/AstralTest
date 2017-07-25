using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Database
{
    public static class DataInitializer
    {
        public static async Task Initialize(this DatabaseContext _context, IServiceProvider serviceProvider)
        {
            //Тут создаются 2 роли(admin и user), если таковых нет
            {
                var roleUser = await _context.Roles.SingleOrDefaultAsync(x => x.RoleName == RolesOption.User.ToString());
                if (roleUser == null)
                {
                    _context.Roles.Add(new Role(RolesOption.User, nameof(RolesOption.User)));
                }

                var roleAdmin = await _context.Roles.SingleOrDefaultAsync(x => x.RoleName == RolesOption.Admin.ToString());
                if (roleAdmin == null)
                {
                    _context.Roles.Add(new Role(RolesOption.Admin, nameof(RolesOption.Admin)));
                }

                if (roleUser == null || roleAdmin == null)
                {
                    await _context.SaveChangesAsync();
                }
            }
            //Тут создаётся пользователь admin, если такового нет
            {
                if (await _context.Users.SingleOrDefaultAsync(x => x.UserName == "admin") == null)
                {
                    var resRole = await _context.Roles.SingleAsync(x => x.RoleName == RolesOption.Admin.ToString());
                    var resultUser = new User { Email = "admin@mail.com", UserName = "admin", RoleId = resRole.RoleId };
                    var hashProvider = serviceProvider.GetRequiredService<IPasswordHasher<User>>();
                    var resultHash = hashProvider.HashPassword(resultUser, "admin");
                    resultUser.PasswordHash = resultHash;
                    _context.Users.Add(resultUser);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}