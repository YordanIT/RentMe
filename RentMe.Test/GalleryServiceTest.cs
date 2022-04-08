using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Core.Services;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentMe.Test
{
    public class GalleryServiceTest
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
                .AddSingleton<IGalleryService, GalleryService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public void GetImagesShouldWork()
        {
            var service = serviceProvider.GetService<IGalleryService>();
            var images = (List<ImageViewModel>)service.GetImages();

            Assert.AreEqual(1, images.Count);
        }

        [Test]
        public async Task AddImageShouldWork()
        {
            var service = serviceProvider.GetService<IGalleryService>();
            var images = new Mock<IFormFileCollection>();
            images.Setup(d => d.GetFile("image.jpg"));
            var model = new ImageFormModel { Description = "test" };

            Assert.DoesNotThrowAsync(async () => await service.AddImage((IFormFileCollection)images, model));
        }

        [Test]
        public async Task AddEmptyImageShouldThrow()
        {
            var service = serviceProvider.GetService<IGalleryService>();
            var images = new FormFileCollection();
            var model = new ImageFormModel { Description = "test" };

            Assert.CatchAsync<ArgumentException>(async () => await service.AddImage(images, model)
            , "There is no image uploaded!");
        }

        [Test]
        public async Task DeleteNotExistingImageShouldThrow()
        {
            var service = serviceProvider.GetService<IGalleryService>();
            var image = new ImageEditModel
            {
                Id = 2
            };

            Assert.CatchAsync<ArgumentException>(async () => await service.DeleteImage(image)
            , "Image does not exist!");
        }

        [Test]
        public async Task DeleteExistingImageShouldNotThrow()
        {
            var service = serviceProvider.GetService<IGalleryService>();
            var image = new ImageEditModel
            {
                Id = 1
            };

            Assert.DoesNotThrowAsync(async () => await service.DeleteImage(image));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var image = new Image
            {
                Id = 1,
                Title = "Test",
                Data = new byte[] { 1, 0, 1},
            };

            await repo.AddAsync(image);
            await repo.SaveChangesAsync();
        }
    }
}
