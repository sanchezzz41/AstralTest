using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Extensions
{
    /// <summary>
    /// Статический класс, предоставляющий методы расширения
    /// </summary>
    public static class ExtensionView
    {
        /// <summary>
        /// Метод расширения для отображения пользоваетелей
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static object UsersView(this IEnumerable<User> users)
        {
            if (users != null)
            {
                return users.Select(x =>
                new
                {
                    Name = x.UserName,
                    Email = x.Email,
                    Id = x.UserId,
                    Notes = x.Notes.Select(n =>
                          new
                          {
                              Text = n.Text,
                              Id = n.Id
                          })
                });
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения пользователей, только аднминам
        /// </summary>
        /// <param name="users"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static object UsersForAdminView(this IEnumerable<User> users, HttpContext httpContext)
        {
            if (httpContext.User.IsInRole(RolesAuthorize.admin.ToString()))
            {
                return users.Select(x =>
                 new
                 {
                     Name = x.UserName,
                     Email = x.Email,
                     Id = x.UserId,
                     Role = x.Role.RoleName,
                     Notes = x.Notes.Select(n =>
                           new
                           {
                               Text = n.Text,
                               Id = n.Id
                           })
                 });
            }
            return null;
        }
        /// <summary>
        /// Метод расширения для отображения заметак
        /// </summary>
        /// <param name="notes"></param>
        /// <returns></returns>
        public static object NotesView(this IEnumerable<Note> notes)
        {
            return notes.Select(x =>
            new
            {
                Id = x.Id,
                Text = x.Text,
                User = x.Master.UserName
            }
            );
        }
    }
}
