using Microsoft.AspNetCore.Mvc.Rendering;
using RentMe.Core.Models;

namespace RentMe.Core.Contracts
{
    public interface IExpenseService
    {
        IEnumerable<ExpenseListViewModel> GetExpenses(TenantViewModel tenant);
        IEnumerable<SelectListItem> GetTenant(ExpenseListViewModel model);
        IEnumerable<SelectListItem> GetTenant(TenantViewModel model);
        Task AddExpense(ExpenseFormModel expense);
        Task DeleteExpense(ExpenseListViewModel expense);
        Task EditExpense(ExpenseListViewModel expense);
    }
}
