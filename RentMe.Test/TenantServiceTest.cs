using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Core.Services;
using RentMe.Infrastructure.Data.Identity;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Test
{
    public class TenantServiceTest
    {
        public class BlogServiceTest
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
                    .AddSingleton<ITenantService, TenantService>()
                    .BuildServiceProvider();

                var repo = serviceProvider.GetService<IApplicationDbRepository>();
                await SeedDbAsync(repo);
            }
            
            [Test]
            public void GetTenantShouldWork()
            {
                var service = serviceProvider.GetService<ITenantService>();

                Assert.IsNotNull(service.GetTenant(new PropertyListViewModel
                {
                    Id = new Guid("9da87b07-668c-4e74-b491-255e7bf020fb")
                }));
            }

            [Test]
            public async Task AddExistingTenantShouldThrow()
            {
                var service = serviceProvider.GetService<ITenantService>();

                Assert.CatchAsync<ArgumentException>(async () =>
                await service.AddTenant(new TenantFormModel
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "mail@mail.com",
                    Address = "Test"
                })
                , "Tenant with this email already exists!");
            }

            [Test]
            public async Task AddNotExistingTenantShouldNotThrow()
            {
                var service = serviceProvider.GetService<ITenantService>();

                Assert.DoesNotThrowAsync(async () =>
                await service.AddTenant(new TenantFormModel
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "mailMail@mail.com",
                    Address = "Test"
                }));
            }

            [Test]
            public async Task RemoveNotExistingTenantShouldThrow()
            {
                var service = serviceProvider.GetService<ITenantService>();

                Assert.CatchAsync<ArgumentException>(async () =>
                await service.RemoveTenant(new TenantViewModel
                {
                    Id = "test"
                })
                , "Tenant does not exist!");
            }

            [Test]
            public async Task GetPropertiesShouldWork()
            {
                var service = serviceProvider.GetService<ITenantService>();

                Assert.NotNull(service.GetProperties(new PropertyListViewModel
                {
                    Id = new Guid("9da87b07-668c-4e74-b491-255e7bf020fb"),
                }));
            }

            public void TearDown()
            {
                dbContext.Dispose();
            }

            private async Task SeedDbAsync(IApplicationDbRepository repo)
            {
                var tenant = new Tenant
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "mail@mail.com",
                    Property = new Property
                    {
                        Id = new Guid("9da87b07-668c-4e74-b491-255e7bf020fb"),
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
                    }
                };

                await repo.AddAsync(tenant);
                await repo.SaveChangesAsync();
            }
        }
    }
}
