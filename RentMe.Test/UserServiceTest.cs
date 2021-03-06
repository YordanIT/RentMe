using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using NUnit.Framework;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Core.Services;
using RentMe.Infrastructure.Data.Identity;
using RentMe.Infrastructure.Data.Repositories;
using System.Threading.Tasks;

namespace RentMe.Test
{
    public class UserServiceTest
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
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task SetNamesShouldWork()
        {
            //var userFormModel = new UserFormModel
            //{
            //    FirstName = "Test",
            //    LastName = "Test"
            //};
                              
            //var service = serviceProvider.GetService<IUserService>();
            //await service.SetNames("Test", userFormModel);
            
            //Assert.AreEqual("Test", userFormModel.FirstName);
            //Assert.AreEqual("Test", userFormModel.LastName);
        }

        [Test]
        public void GetUsersShouldWork()
        {
            //var service = serviceProvider.GetService<IUserService>();
            //var users = service.GetUsers();

            //Assert.AreEqual(1, users);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var user = new ApplicationUser
            {
                Email = "test@mail.com",
                PasswordHash = "w85$62Md",
                EmailConfirmed = true,
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}