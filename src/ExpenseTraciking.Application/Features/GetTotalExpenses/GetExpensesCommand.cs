using ExpenseTraciking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTraciking.Application.Features.GetTotalExpenses
{
    public class GetExpensesCommand : IRequest<List<Expense>>
    {
        public int UserId { get; set; }

    }
}
