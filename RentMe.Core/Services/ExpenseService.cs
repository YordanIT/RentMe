using Microsoft.AspNetCore.Mvc.Rendering;
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
            var tenant = await repo.All<Tenant>().FirstAsync(t => t.Email == model.TenantEmail);
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
              PropertyId = propertyId,
              Tenant = tenant
            };

            tenant.Expenses.Add(expense);
            repo.Update(tenant);

            await repo.AddAsync(expense);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteExpense(ExpenseListViewModel model)
        {
            var expense = await repo.All<Expense>()
               .Where(e => e.IsDeleted == false)
               .FirstOrDefaultAsync(e => e.Id == model.Id);

            if (expense == null)
            {
                throw new ArgumentException("Expense does not exist!");
            }

            expense.IsDeleted = true;

            await repo.SaveChangesAsync();
        }

        public async Task EditExpense(ExpenseListViewModel model)
        {
            var expense = await repo.All<Expense>()
               .Where(e => e.IsDeleted == false)
               .FirstOrDefaultAsync(e => e.Id == model.Id);

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
                .First(t => t.Id == model.Id);

            var expenses = repo.All<Expense>()
                .Where(e => e.TenantId == tenant.Id)
                .Where(e => e.IsDeleted == false)
                .ToList()
                .Select(e => new ExpenseListViewModel
                {
                    Id = e.Id,
                    IsPaid = e.IsPaid,
                    Rent = e.Rent,
                    Electricity = e.Electricity,
                    Heating = e.Heating,
                    Water = e.Water,
                    EntranceFee = e.EntranceFee,
                    Other = (decimal)(e.Other == null ? 0 : e.Other),
                    Comment = e.Comment,
                    PropertyId = e.PropertyId.ToString(),
                }).ToList();

            return expenses;
        }

        public IEnumerable<SelectListItem> GetTenant(ExpenseListViewModel model)
        {
            var tenants = repo.All<Tenant>()
               .Where(t => t.IsDeleted == false)
               .Where(t => t.PropertyId.ToString() == model.PropertyId)
               .Select(t => new SelectListItem
               {
                   Text = t.Email,
               }).ToList();

            return tenants;
        }

        public IEnumerable<SelectListItem> GetTenant(TenantViewModel model)
        {
            var tenants = repo.All<Tenant>()
               .Where(t => t.IsDeleted == false)
               .Where(t => t.Id == model.Id)
               .Select(t => new SelectListItem
               {
                   Text = t.Email,
               }).ToList();

            return tenants;
        }
    }
}
