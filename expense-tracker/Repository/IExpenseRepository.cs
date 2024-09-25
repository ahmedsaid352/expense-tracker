using expense_tracker.Data.Models;

namespace expense_tracker.Repository
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetExpensesForUserAsync(string username, DateTime? startDate, DateTime? endDate);
        Task<Expense> GetExpenseByIdAsync(int id, string username);
        Task AddExpenseAsync(Expense expense);
        Task UpdateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(int id, string username);
    }
}
