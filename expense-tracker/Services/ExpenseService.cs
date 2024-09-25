using expense_tracker.Data.DTOs;
using expense_tracker.Data.Models;
using expense_tracker.Helpers;
using expense_tracker.Repository;

namespace expense_tracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;

        public ExpenseService(IExpenseRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ExpenseDTO>> GetExpensesForUserAsync(string username, DateTime? startDate, DateTime? endDate)
        {

            var expenses = await _repository.GetExpensesForUserAsync(username, startDate, endDate);
            var expenseDTOs = expenses.Select(e => Mapper.ConvertExpenseToDTO(e)).ToList();
            return expenseDTOs;
        }

        public async Task<ExpenseDTO> GetExpenseByIdAsync(int id, string username)
        {
            var expense = await _repository.GetExpenseByIdAsync(id, username);
            return Mapper.ConvertExpenseToDTO(expense);
        }

        public async Task AddExpenseAsync(ExpenseDTO expenseDTO, string username)
        {
            var expense = Mapper.ConvertDTOToExpense(expenseDTO, username);
            await _repository.AddExpenseAsync(expense);
        }

        public async Task UpdateExpenseAsync(ExpenseDTO expenseDTO, string username)
        {
            var existingExpense = await _repository.GetExpenseByIdAsync(expenseDTO.Id, username);
            if (existingExpense != null)
            {
                existingExpense.Description = expenseDTO.Description;
                existingExpense.Amount = expenseDTO.Amount;
                existingExpense.Date = expenseDTO.Date;
                existingExpense.CategoryId = expenseDTO.CategoryId;
                await _repository.UpdateExpenseAsync(existingExpense);
            }
        }

        public async Task DeleteExpenseAsync(int id, string username)
        {
            await _repository.DeleteExpenseAsync(id, username);
        }
    }
}
