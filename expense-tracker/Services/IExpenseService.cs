using expense_tracker.Data.DTOs;
using expense_tracker.Data.Models;

namespace expense_tracker.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDTO>> GetExpensesForUserAsync(string username, DateTime? startDate, DateTime? endDate);
        Task<ExpenseDTO> GetExpenseByIdAsync(int id, string username);
        Task AddExpenseAsync(ExpenseDTO expenseDTO, string username);
        Task UpdateExpenseAsync(ExpenseDTO expenseDTO, string username);
        Task DeleteExpenseAsync(int id, string username);
    }
}

