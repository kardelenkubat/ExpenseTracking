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
        // GET: api/expenses
        [HttpGet("expense/{expenseId}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenseById(int expenseId)
        {
            var expense = _expenseRepository.GetExpenseById(expenseId);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPost]
        public IActionResult AddExpense([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest();
            }

            _expenseRepository.AddExpense(expense);
            return CreatedAtAction(nameof(GetExpenseById), new { expenseId = expense.Id }, expense);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, [FromBody] Expense expense)
        {
            if (expense == null || expense.Id != id)
            {
                return BadRequest();
            }

            var existingExpense = _expenseRepository.GetExpenseById(id);
            if (existingExpense == null)
            {
                return NotFound();
            }

            _expenseRepository.UpdateExpense(expense);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            var expense = _expenseRepository.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }

            _expenseRepository.DeleteExpense(id);
            return NoContent();
        }

    }
}

