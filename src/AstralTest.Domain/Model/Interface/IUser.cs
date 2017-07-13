using AstralTest.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain
{
      public interface IUser
    {
        IEnumerable<User> Users { get; }
        void AddUser(User user);
        void DeleteUser(User user);
        void EditUser(User user);
    }
}
