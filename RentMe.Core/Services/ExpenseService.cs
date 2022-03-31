using Microsoft.EntityFrameworkCore;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;

namespace RentMe.Core.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IApplicationDbRepository repo;

        public ExpenseService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddExpense(ExpenseFormModel model)
        {
            var tenant = await repo.All<Tenant>().FirstAsync(t => t.Email == model.Tenant);
            var propertyId = tenant.PropertyId;

            var expense = new Expense
            {
              Rent = model.Rent,
              Electricity = model.Electricity,
              EntranceFee = model.EntranceFee,
              Heating = model.Heating,
              Water = model.Water,
              Other = model.Other,
              Comment = model.Comment,
              PropertyId = propertyId
            };

            tenant.Expenses.Add(expense);

            await repo.AddAsync(expense);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteExpense(ExpenseListViewModel model)
        {
            var expense = await repo.All<Expense>()
               .Where(e => e.IsDeleted == false)
               .FirstOrDefaultAsync(e => e.Id.ToString() == model.Id);

            if (expense == null)
            {
                throw new ArgumentException("Expense does not exist!");
            }

            expense.IsDeleted = true;

            await repo.SaveChangesAsync();
        }

        public async Task EditExpense(ExpenseEditViewModel model)
        {
            var expense = await repo.All<Expense>()
               .Where(e => e.IsDeleted == false)
               .FirstOrDefaultAsync(e => e.Id.ToString() == model.Id);

            if (expense == null)
            {
                throw new ArgumentException("Expense does not exist!");
            }

            expense.IsPaid = true;

            await repo.SaveChangesAsync();
        }

        public IEnumerable<ExpenseListViewModel> GetExpenses(TenantViewModel model)
        {
            var tenant = repo.All<Tenant>()
                .First(t => t.Id.ToString() == model.Id);

            var expenses = tenant.Expenses
                .Where(e => e.IsDeleted == false)
                .ToList()
                .Select(e => new ExpenseListViewModel
                {
                    Id = e.Id.ToString(),
                    IsPaid = e.IsPaid,
                    Rent = e.Rent,
                    Electricity = e.Electricity,
                    Heating = e.Heating,
                    Water = e.Water,
                    EntranceFee = e.EntranceFee,
                    Other = e.Other,
                    Comment = e.Comment,
                    PropertyId = e.PropertyId.ToString()
                })
                .OrderBy(e => e.IsPaid)
                .ToList();

            return expenses;
        }
    }
}
