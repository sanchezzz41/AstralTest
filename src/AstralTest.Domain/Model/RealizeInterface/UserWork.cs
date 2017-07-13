using AstralTest.Domain.Context;
using AstralTest.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Model.RealizeInterface
{
    public class UserWork : IUser
    {
        private AstralContext _context { get; }

        public IEnumerable<User> Users
        {
            get
            {
                return _context.Users.Include(x => x.Notes).ToList();
            }
        }

        public UserWork(AstralContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            if(user!=null)
            {
                if(_context.Users.Count(x=>x.Id==user.Id)==0)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("User with same Id is exist");
                }
            }
            else
            {
                throw new Exception("User is null");
            }
        }

        public void EditUser(User user)
        {
            if (user != null)
            {
                var result = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (result != null)
                {
                    result.Name = user.Name;
                    _context.Users.Attach(result);
                    _context.Entry(result).Property(x => x.Name).IsModified = true;
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("User with same Id is not exist");
                }
            }
            else
            {
                throw new Exception("User is null");
            }
        }

        public void DeleteUser(User user)
        {
            if (user != null)
            {
                var result = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (result!=null)
                {
                    _context.Users.Remove(result);
                    foreach (var item in result.Notes)
                    {
                        _context.Notes.Remove(item);
                    }
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("User with same Id is not exist");
                }
            }
            else
            {
                throw new Exception("User is null");
            }
        }
    }
}
