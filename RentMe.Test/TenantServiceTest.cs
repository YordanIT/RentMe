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
            private Property property;
            private Tenant tenant;


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

                property = repo.All<Tenant>().Single().Property;
                tenant = repo.All<Tenant>().Single();
            }
            
            [Test]
            public void GetTenantShouldWork()
            {
                var propertyViewModel = new PropertyListViewModel 
                { 
                    Id = property.Id,
                    Area = 100,
                    Floor = 1,
                };
                var service = serviceProvider.GetService<ITenantService>();

                Assert.IsNotNull(service.GetTenant(propertyViewModel));
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
                    Id = new Guid()
                })
                , "Tenant does not exist!");
            }

            [Test]
            public async Task RemoveExistingTenantShouldNotThrow()
            {
                var service = serviceProvider.GetService<ITenantService>();

                Assert.DoesNotThrowAsync(async () => await service.RemoveTenant(new TenantViewModel{ Id = tenant.Id }));
                Assert.IsTrue(tenant.IsDeleted);
            }

            [Test]
            public async Task GetPropertiesShouldWork()
            {
                var propertyViewModel = new PropertyListViewModel
                {
                    Id = property.Id,
                    Area = 100,
                    Floor = 1,
                };

                var service = serviceProvider.GetService<ITenantService>();

                Assert.NotNull(service.GetProperties(propertyViewModel));
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
                    }
                };

                await repo.AddAsync(tenant);
                await repo.SaveChangesAsync();
            }
        }
    }
}
