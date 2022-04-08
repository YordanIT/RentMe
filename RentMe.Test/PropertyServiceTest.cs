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
    public class PropertyServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private ApplicationUser user;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IPropertyService, PropertyService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);


            user = new ApplicationUser
            {
                Id = "9da87b07-668c-4e74-b491-255e7bf020fb",
                Email = "test@mail.com",
                PasswordHash = "w85$62Md",
                EmailConfirmed = true,
            };
        }

        [Test]
        public async Task AddExistingPropertyTypeShowTrow()
        {
            var type = new PropertyTypeFormModel { Type = "Test" };

            var service = serviceProvider.GetService<PropertyService>();

            Assert.CatchAsync<ArgumentException>(async () =>
            await service.AddPropertyType(type), "PropertyType already exists!");
        }

        [Test]
        public async Task AddNotExistingPropertyTypeShowNotTrow()
        {
            var type = new PropertyTypeFormModel { Type = "TestTest" };

            var service = serviceProvider.GetService<PropertyService>();

            Assert.DoesNotThrow(async () => await service.AddPropertyType(type));
        }

        [Test]
        public async Task AddExistingPropertyShowTrow()
        {
            var property = new PropertyFormModel
            {
                Address = "Test",
                Area = 100,
                City = "Test",
                Floor = 1,
                Type = "Test",
                HasElevator = true,
                HasFurniture = true,
                HasParking = true
            };

            var service = serviceProvider.GetService<PropertyService>();

            Assert.CatchAsync<ArgumentException>(async () =>
            await service.AddProperty(property, user), "Property with this address already exists!");
        }

        [Test]
        public async Task AddNotExistingPropertyShowNotTrow()
        {
            var property = new PropertyFormModel
            {
                Address = "TestTest",
                Area = 100,
                City = "Test",
                Floor = 1,
                Type = "Test",
                HasElevator = true,
                HasFurniture = true,
                HasParking = true
            };

            var service = serviceProvider.GetService<PropertyService>();

            Assert.DoesNotThrowAsync(async () => await service.AddProperty(property, user));
        }

        [Test]
        public async Task DeleteExistingPropertyShowNotTrow()
        {
            var property = new PropertyListViewModel
            {
                Id = new Guid("9da87b07-668c-4e74-b491-255e7bf020fb"),
                Address = "Test",
                Area = 100,
                City = "Test",
                Floor = 1,
                Type = "Test",
                HasElevator = true,
                HasFurniture = true,
                HasParking = true
            };

            var service = serviceProvider.GetService<PropertyService>();

            Assert.DoesNotThrowAsync(async () => await service.DeleteProperty(property));
        }

        [Test]
        public async Task DeleteNotExistingPropertyShowTrow()
        {
            var property = new PropertyListViewModel
            {
                Id = new Guid("0da87b07-668c-4e74-b491-255e7bf020fb"),
                Address = "TestTest",
                Area = 100,
                City = "Test",
                Floor = 1,
                Type = "Test",
                HasElevator = true,
                HasFurniture = true,
                HasParking = true
            };

            var service = serviceProvider.GetService<PropertyService>();

            Assert.CatchAsync<ArgumentException>(async () =>
            await service.DeleteProperty(property), "Property does not exist!");
        }

        [Test]
        public void GetPropertiesShouldWork()
        {
            var service = serviceProvider.GetService<PropertyService>();
            var properties = (List<PropertyListViewModel>)service.GetProperties(user);

            Assert.AreEqual(1, properties.Count);
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
            };

            await repo.AddAsync(property);
            await repo.SaveChangesAsync();
        }
    }
}
