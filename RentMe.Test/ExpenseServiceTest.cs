using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Core.Services;
using RentMe.Infrastructure.Data.Identity;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Test
{
    public class ExpenseServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private Tenant tenant;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IExpenseService, ExpenseService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);

            tenant = repo.All<Tenant>().Single();
        }
        
        [Test]
        public void GetExpensesShouldWork()
        {
            var tenantViewModel = new TenantViewModel { Id = tenant.Id };
            var service = serviceProvider.GetService<IExpenseService>();

            var expenses = (List<ExpenseListViewModel>)service.GetExpenses(tenantViewModel);

            Assert.AreEqual(1, expenses.Count);
        }

        [Test]
        public  void AddExpenseShouldWork()
        {
            var expense = new ExpenseFormModel
            {
                Rent = 101,
                Electricity = 1,
                EntranceFee = 1,
                Heating = 1,
                Water = 1,
                Other = 1,
                Comment = "Test",
                TenantEmail = "Test@mail.com"
            };

            var service = serviceProvider.GetService<IExpenseService>();
            var expenses = service.AddExpense(expense);

            Assert.AreEqual(expense.Rent, tenant.Expenses.Single(t => t.Rent == 101).Rent);
        }

        [Test]
        public async Task DeleteExistingExpenseShouldNotThrow()
        {
            var id = tenant.Expenses.Single().Id;
            var expense = new ExpenseListViewModel { Id = id };
            var service = serviceProvider.GetService<IExpenseService>();

            Assert.DoesNotThrowAsync(async () => await service.DeleteExpense(expense));
            Assert.IsTrue(tenant.Expenses.Single().IsDeleted);
        }

        [Test]
        public async Task DeleteNotExistingExpenseShouldThrow()
        {
            var expense = new ExpenseListViewModel { Id = new Guid() };
            var service = serviceProvider.GetService<IExpenseService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.DeleteExpense(expense)
            , "Expense does not exist!");
            Assert.IsFalse(tenant.Expenses.Single().IsDeleted);
        }

        [Test]
        public async Task EditExistingExpenseShouldNotThrow()
        {
            var id = tenant.Expenses.Single().Id;
            var expense = new ExpenseListViewModel { Id = id };
            var service = serviceProvider.GetService<IExpenseService>();

            Assert.DoesNotThrowAsync(async () => await service.EditExpense(expense));
            Assert.IsTrue(tenant.Expenses.Single().IsPaid);
        }

        [Test]
        public async Task EditNotExistingExpenseShouldThrow()
        {
            var expense = new ExpenseListViewModel { Id = new Guid() };
            var service = serviceProvider.GetService<IExpenseService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.EditExpense(expense)
            , "Expense does not exist!");
            Assert.IsFalse(tenant.Expenses.Single().IsPaid);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var property = new Property
            {
                PropertyType = new PropertyType { Type = "Test" },
                Address = "Test",
                Area = 100,
                City = "Test",
                Floor = 1,
                HasParking = true,
                HasElevator = true,
                HasFurniture = true,
                ApplicationUser = new ApplicationUser
                {
                    Email = "test@mail.com",
                    PasswordHash = "w85$62Md",
                    EmailConfirmed = true,
                }
            };

            var tenant = new Tenant
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@mail.com",
                Phone = "88881000",
                Property = property
            };

            var expense = new Expense
            {
                Rent = 1,
                Electricity = 1,
                EntranceFee = 1,
                Heating = 1,
                Water = 1,
                Other = 1,
                Comment = "Test",
                Property = property,
                Tenant = tenant
            };

            await repo.AddAsync(property);
            await repo.AddAsync(tenant);
            await repo.AddAsync(expense);
            await repo.SaveChangesAsync();
        }
    }
}
