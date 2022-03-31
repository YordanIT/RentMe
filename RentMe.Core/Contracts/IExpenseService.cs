using RentMe.Core.Models;

namespace RentMe.Core.Contracts
{
    public interface IExpenseService
    {
        IEnumerable<ExpenseListViewModel> GetExpenses(TenantViewModel tenant);
        Task AddExpense(ExpenseFormModel expense);
        Task DeleteExpense(ExpenseListViewModel expense);
        Task EditExpense(ExpenseEditViewModel expense);
    }
}
