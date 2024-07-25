using ExpenseTraciking.Application.Interfaces;
using ExpenseTraciking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTraciking.Application.Features.GetTotalExpenses
{
    public class GetExpensesCommandHandler : IRequestHandler<GetExpensesCommand, List<Expense>>
    {
        private readonly IExpenseRepository expenseRepository;

        public GetExpensesCommandHandler(IExpenseRepository expenseRepository) {
            this.expenseRepository = expenseRepository;
        }
        public Task<List<Expense>> Handle(GetExpensesCommand request, CancellationToken cancellationToken)
        {
          var userExpenses = expenseRepository.GetAllExpenses().Where(x => x.UserId == request.UserId).ToList();
            return Task.FromResult(userExpenses);
        }
    }
}
