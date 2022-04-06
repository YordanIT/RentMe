using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Core.Services;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace RentMe.Test
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
                .AddSingleton<IBlogService, BlogService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task ArticleWithSameTitleShouldThrow()
        {
            var article = new ArticleFormModel
            {
                Title = "Test",
                Content = "Test",
            };

            var service = serviceProvider.GetService<IBlogService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.AddArticle(article)
            , "Article with this title already exists or text is too long!");
        }

        [Test]
        public async Task ArticleWithDifferentTitleShouldNotThrow()
        {
            var article = new ArticleFormModel
            {
                Title = "TestTest",
                Content = "Test",
            };

            var service = serviceProvider.GetService<IBlogService>();

            Assert.DoesNotThrowAsync(async () => await service.AddArticle(article));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var article = new Article
            {
                Title = "Test",
                Content = "Test"
            };

            await repo.AddAsync(article);
            await repo.SaveChangesAsync();
        }
    }
}
