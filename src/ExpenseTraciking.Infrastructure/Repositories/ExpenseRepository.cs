using ExpenseTraciking.Application.Interfaces;
using ExpenseTraciking.Domain.Entities;

using ExpenseTraciking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Expense> GetAllExpenses()
        {
            return _context.Expenses;
        }

        public Expense GetExpenseById(int id)
        {
            return _context.Expenses.Find(id)!;
        }

        public void AddExpense(Expense expense)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Expenses.Add(expense);
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


        public void UpdateExpense(Expense expense)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingExpense = _context.Expenses.Find(expense.Id);
                    if (existingExpense != null)
                    {
                        _context.Entry(existingExpense).CurrentValues.SetValues(expense);
                        _context.SaveChangesAsync();

                        
                        transaction.Commit();
                    }
                    else
                    {
                        throw new InvalidOperationException("Expense not found.");
                    }
                }
                catch (Exception)
                {
                    
                    transaction.Rollback();
                    throw;
                }
            }
        }



        public void DeleteExpense(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var expense = _context.Expenses.Find(id);
                    if (expense != null)
                    {
                        _context.Expenses.Remove(expense);
                        _context.SaveChangesAsync();

                        
                        transaction.Commit();
                    }
                    else
                    {
                        throw new InvalidOperationException("Expense not found.");
                    }
                }
                catch (Exception)
                {
                    
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<Expense> GetExpensesByUserIdAndDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            return _context.Expenses
                .Where(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate)
                .ToList();
        }

    }
}
