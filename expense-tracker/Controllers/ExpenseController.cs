using expense_tracker.Data.DTOs;
using expense_tracker.Data.Models;
using expense_tracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using expense_tracker.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace expense_tracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses(DateTime? startDate, DateTime? endDate)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null)
            {
                return Unauthorized();
            }
            var expenseDTOs = await _service.GetExpensesForUserAsync(username,startDate, endDate);
            return Ok(expenseDTOs);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(int id)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null)
            {
                return Unauthorized();
            }
            var expenseDTO = await _service.GetExpenseByIdAsync(id, username);
            if (expenseDTO == null)
            {
                return NotFound();
            }
            return Ok(expenseDTO);
        }



        [HttpPost]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseDTO expenseDTO)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            await _service.AddExpenseAsync(expenseDTO, username);
            return CreatedAtAction(nameof(GetExpense), new { id = expenseDTO.Id }, expenseDTO);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDTO updatedExpenseDTO)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(username)){
                return Unauthorized();
            }

            var expense = await _service.GetExpenseByIdAsync(id, username);
            if (expense == null)
            {
                return NotFound();
            }

            updatedExpenseDTO.Id = id; 
            await _service.UpdateExpenseAsync(updatedExpenseDTO, username);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            var expense = await _service.GetExpenseByIdAsync(id, username);
            if (expense == null)
            {
                return NotFound();
            }

            await _service.DeleteExpenseAsync(id, username);
            return NoContent();
        }
    }
}
