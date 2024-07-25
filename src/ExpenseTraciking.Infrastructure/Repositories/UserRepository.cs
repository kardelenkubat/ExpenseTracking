using ExpenseTraciking.Application.Interfaces;
using ExpenseTraciking.Domain.Entities;
using ExpenseTraciking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTraciking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id)!;
        }

        public void AddUser(User user)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingUser = _context.Users.Find(user.Id);
                    if (existingUser != null)
                    {
                        existingUser.Username = user.Username;
                        existingUser.Email = user.Email;
                        existingUser.Password = user.Password;
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _context.Users.Find(id);
                    if (user != null)
                    {
                        _context.Users.Remove(user);
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
