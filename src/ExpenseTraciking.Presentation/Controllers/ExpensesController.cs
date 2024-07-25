using ExpenseTraciking.Application.Features.GetTotalExpenses;
using ExpenseTraciking.Application.Interfaces;
using ExpenseTraciking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTraciking.Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ISender _mediatR;

        public ExpensesController(IExpenseRepository expenseRepository, ISender mediatR)
        {
            _expenseRepository = expenseRepository;
            _mediatR = mediatR;
        }

        [HttpGet("user/{UserId}")]
        public async Task<ActionResult<List<Expense>>> GetUserExpense(int UserId)
        {
            var expense = await _mediatR.Send(new GetExpensesCommand { UserId = UserId });
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }
    //    // GET: api/expenses
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
    //    {
    //        var expenses = await _expenseRepository.GetAllExpensesAsync();
    //        return Ok(expenses);
    //    }

    //    // GET: api/expenses/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Expense>> GetExpense(int id)
    //    {
    //        var expense = await _expenseRepository.GetExpenseByIdAsync(id);
    //        if (expense == null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(expense);
    //    }

    //    // POST: api/expenses
    //    [HttpPost]
    //    public async Task<ActionResult<Expense>> PostExpense(Expense expense)
    //    {
    //        await _expenseRepository.AddExpenseAsync(expense);
    //        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    //    }

    //    // PUT: api/expenses/5
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutExpense(int id, Expense expense)
    //    {
    //        if (id != expense.Id)
    //        {
    //            return BadRequest();
    //        }

    //        await _expenseRepository.UpdateExpenseAsync(expense);
    //        return NoContent();
    //    }

    //    // DELETE: api/expenses/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteExpense(int id)
    //    {
    //        var expense = await _expenseRepository.GetExpenseByIdAsync(id);
    //        if (expense == null)
    //        {
    //            return NotFound();
    //        }

    //        await _expenseRepository.DeleteExpenseAsync(id);
    //        return NoContent();
    //    }
    }
}

