using ExpenseTraciking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTraciking.Application.Interfaces
{
    public interface IExpenseRepository
    {
        public IQueryable<Expense> GetAllExpenses();


        public Expense GetExpenseById(int id);


        public void AddExpense(Expense expense);


        public void UpdateExpense(Expense expense);


        public void DeleteExpense(int id);
        IEnumerable<Expense> GetExpensesByUserIdAndDateRange(int userId, DateTime startDate, DateTime endDate);

    }
}
