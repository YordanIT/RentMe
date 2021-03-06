using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Core.Services;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Test
{
    public class BlogServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private Article article;
        
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

            article = repo.All<Article>().Single();
        }

        [Test]
        public async Task AddArticleWithSameTitleShouldThrow()
        {
            var articleFormModel = new ArticleFormModel
            {
                Title = article.Title,
                Content = "Test",
            };

            var service = serviceProvider.GetService<IBlogService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.AddArticle(articleFormModel)
            , "Article with this title already exists or text is too long!");
        }

        [Test]
        public async Task AddArticleWithDifferentTitleShouldNotThrow()
        {
            var articleFormModel = new ArticleFormModel
            {
                Title = "TestTest",
                Content = "Test",
            };

            var service = serviceProvider.GetService<IBlogService>();

            Assert.DoesNotThrowAsync(async () => await service.AddArticle(articleFormModel));
        }

        [Test]
        public async Task DeleteNotExistingArticleShouldThrow()
        {
            var articleViewModel = new ArticleViewModel
            {
                Title = "TestTest",
                Content = "Test",
            };

            var service = serviceProvider.GetService<IBlogService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.DeleteArticle(articleViewModel)
            , "Article does not exist!");
        }

        [Test]
        public async Task DeleteExistingArticleShouldNotThrow()
        {
            var articleViewModel = new ArticleViewModel
            {
                Id = article.Id,
                Title = "Test",
                Content = "Test",
            };

            var service = serviceProvider.GetService<IBlogService>();

            Assert.DoesNotThrowAsync(async () => await service.DeleteArticle(articleViewModel));
            Assert.IsTrue(article.IsDeleted);
        }

        [Test]
        public void GetArticlesShouldWork()
        {
            var service = serviceProvider.GetService<IBlogService>();
            var articles = (List<ArticleViewModel>)service.GetArticles();

            Assert.AreEqual(1, articles.Count);
        }

        [Test]
        public void GetArticlesShouldReturnNull()
        {
            var service = serviceProvider.GetService<IBlogService>();
            service.DeleteArticle(new ArticleViewModel 
            {
                Id = article.Id,
                Title = "Test",
                Content = "Test"
            });
            var articles = (List<ArticleViewModel>)service.GetArticles();

            Assert.AreEqual(0, articles.Count);
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
                Id = 1,
                Title = "Test",
                Content = "Test"
            };

            await repo.AddAsync(article);
            await repo.SaveChangesAsync();
        }
    }
}
