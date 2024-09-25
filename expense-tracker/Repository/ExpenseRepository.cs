using expense_tracker.Data.Models;
using expense_tracker.Data;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetExpensesForUserAsync(string username, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Expenses
                .Where(e => e.Username == username);

            // If a date range is provided
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(e => e.Date >= startDate.Value && e.Date <= endDate.Value);
            }
            else if (endDate == null && startDate.HasValue) {
                query = query.Where(e => e.Date >= startDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int id, string username)
        {
            return await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.Username == username);
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(int id, string username)
        {
            var expense = await GetExpenseByIdAsync(id, username);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }
    }
}
