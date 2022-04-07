using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

        //[Test]
        //public async Task SetNamesShouldWork()
        //{
        //    var user = new UserFormModel
        //    {
        //        FirstName = "Test",
        //        LastName = "Test"
        //    };

        //    var service = serviceProvider.GetService<IUserService>();
        //    await service.SetNames("Test", user);

        //    Assert.AreEqual("Test", user.FirstName);
        //    Assert.AreEqual("Test", user.LastName);
        //}

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var user = new ApplicationUser
            {
                Id = "Test",
                Email = "Test@mail.com",
                EmailConfirmed = true,
                ConcurrencyStamp = "test",
                PasswordHash = "test",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}