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
            _context.Expenses.Add(expense);
             _context.SaveChanges();
        }

        public void UpdateExpense(Expense expense)
        {
            _context.Expenses.Update(expense);
            _context.SaveChangesAsync();
        }

        public void DeleteExpense(int id)
        {
            var expense = _context.Expenses.Find(id);
            _context.Expenses.Remove(expense!);
            _context.SaveChangesAsync();
        }

      
    }
}
