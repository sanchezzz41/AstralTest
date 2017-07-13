using AstralTest.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain
{
    interface IUser
    {
        void AddUser(User user);
        void DeleteUser(User user);
        void ChangeUser(User user);
    }
}
