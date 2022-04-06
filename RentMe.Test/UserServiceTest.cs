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
        private ApplicationUser user;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);

            user = await repo.All<ApplicationUser>().FirstOrDefaultAsync();
        }

        [Test]
        public async Task SetNamesShouldWork()
        {
            var testUser = new UserFormModel
            {
                FirstName = "Test",
                LastName = "Test"
            };

            var service = serviceProvider.GetService<IUserService>();
            await service.SetNames(user.Id, testUser);

            Assert.Equals(user.FirstName, testUser.FirstName);
            Assert.Equals(user.LastName, testUser.LastName);
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
                FirstName = "Test",
                LastName = "Test"
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}