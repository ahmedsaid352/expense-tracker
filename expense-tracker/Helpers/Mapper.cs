using expense_tracker.Data.DTOs;
using expense_tracker.Data.Models;

namespace expense_tracker.Helpers
{
    public static class Mapper
    {
        public static ExpenseDTO ConvertExpenseToDTO(Expense expense)
        {
            return new ExpenseDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CategoryId = expense.CategoryId
            };
        }

        public static Expense ConvertDTOToExpense(ExpenseDTO expenseDTO, String username) {
            return new Expense
            {
                Description = expenseDTO.Description,
                Amount = expenseDTO.Amount,
                Date = expenseDTO.Date,
                CategoryId = expenseDTO.CategoryId,
                Username = username // from Claims
            };
        }
    }

}
