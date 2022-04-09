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
    public class PropertyServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private ApplicationUser user;
        private Property property;

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

            user = repo.All<ApplicationUser>().Single();
            property = repo.All<Property>().Single();
        }

        [Test]
        public async Task AddExistingPropertyTypeShouldTrow()
        {
            var type = new PropertyTypeFormModel { Type = "Test" };
            var service = serviceProvider.GetService<IPropertyService>();

            Assert.CatchAsync<ArgumentException>(async () =>
            await service.AddPropertyType(type), "PropertyType already exists!");
        }

        [Test]
        public async Task AddNotExistingPropertyTypeShouldNotTrow()
        {
            var type = new PropertyTypeFormModel { Type = "TestTest" };
            var service = serviceProvider.GetService<IPropertyService>();

            Assert.DoesNotThrowAsync(async () => await service.AddPropertyType(type));
        }

        [Test]
        public async Task AddExistingPropertyShouldTrow()
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

            var service = serviceProvider.GetService<IPropertyService>();

            Assert.CatchAsync<ArgumentException>(async () =>
            await service.AddProperty(property, user), "Property with this address already exists!");
        }

        [Test]
        public async Task AddNotExistingPropertyShouldNotTrow()
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

            var service = serviceProvider.GetService<IPropertyService>();

            Assert.DoesNotThrowAsync(async () => await service.AddProperty(property, user));
        }

        [Test]
        public async Task DeleteExistingPropertyShowNotTrow()
        {
            var propertyViewModel = new PropertyListViewModel
            {
                Id = property.Id,
                Area = 100,
                Floor = 1,
            };

            var service = serviceProvider.GetService<IPropertyService>();

            Assert.DoesNotThrowAsync(async () => await service.DeleteProperty(propertyViewModel));
            Assert.IsTrue(property.IsDeleted);
        }

        [Test]
        public async Task DeleteNotExistingPropertyShouldTrow()
        {
            var propertyViewModel = new PropertyListViewModel
            {
                Id = new Guid(),
                Area = 100,
                Floor = 1,
            };

            var service = serviceProvider.GetService<IPropertyService>();

            Assert.CatchAsync<ArgumentException>(async () =>
            await service.DeleteProperty(propertyViewModel), "Property does not exist!");
        }

        [Test]
        public void GetPropertiesShouldWork()
        {
            var service = serviceProvider.GetService<IPropertyService>();
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

            await repo.AddAsync(property);
            await repo.SaveChangesAsync();
        }
    }
}
