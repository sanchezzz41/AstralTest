using AstralTest.DataDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Interface
{
      public interface IUser
    {
        IEnumerable<User> Users { get; }
        void AddUser(User user);
        void DeleteUser(User user);
        void EditUser(User user);
    }
}
