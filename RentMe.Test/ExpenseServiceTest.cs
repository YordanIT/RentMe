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
using System.Threading.Tasks;

namespace RentMe.Test
{
    public class ExpenseServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

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
        }

        //[Test]
        //public void GetExpensesShouldWork()
        //{
        //    var service = serviceProvider.GetService<IExpenseService>();
        //    var tenant = new TenantViewModel { Id = "9da87b07-668c-4e74-b491-255e7bf020fb" };
        //    var expenses = (List<ExpenseListViewModel>)service.GetExpenses(tenant);

        //    Assert.AreEqual(1, expenses.Count);
        //}

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var expense = new Expense
            {
                Rent = 1,
                Electricity = 1,
                EntranceFee = 1,
                Heating = 1,
                Water = 1,
                Other = 1,
                Comment = "Test",
                Property = new Property
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
                        Id = "9da87b07-668c-4e74-b491-255e7bf020fb",
                        Email = "test@mail.com",
                        PasswordHash = "w85$62Md",
                        EmailConfirmed = true,
                    }
                },
                Tenant = new Tenant
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "Test@mail.com",
                    Phone = "88881000",
                }
            };

            await repo.AddAsync(expense);
            await repo.SaveChangesAsync();
        }
    }
}
