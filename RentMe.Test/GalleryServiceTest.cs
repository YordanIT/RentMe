using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Core.Services;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Test
{
    public class GalleryServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private Image image;

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

            image = repo.All<Image>().Single();
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
            var mock = GetFormFileCollection();
            var model = new ImageFormModel { Description = "test" };

            var service = serviceProvider.GetService<IGalleryService>();
            
            Assert.DoesNotThrowAsync(async () => await service.AddImage(mock, model));
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
            var imageViewModel = new ImageEditModel
            {
                Id = 2
            };

            Assert.CatchAsync<ArgumentException>(async () => await service.DeleteImage(imageViewModel)
            , "Image does not exist!");
        }

        [Test]
        public async Task DeleteExistingImageShouldNotThrow()
        {
            var service = serviceProvider.GetService<IGalleryService>();
            var imageViewModel = new ImageEditModel
            {
                Id = image.Id
            };

            Assert.DoesNotThrowAsync(async () => await service.DeleteImage(imageViewModel));
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

        private static IFormFileCollection GetFormFileCollection()
        {
            var filesFolder = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}UploadFiles\\";
            List<string> filesPathsListToUpload = new List<string>();
            filesPathsListToUpload.Add($"{filesFolder}UploadFile1.png");
            filesPathsListToUpload.Add($"{filesFolder}UploadFile2.jpg");
            filesPathsListToUpload.Add($"{filesFolder}UploadFile3.bmp");
            FormFileCollection filesCollection = new FormFileCollection();

            foreach (var filePath in filesPathsListToUpload)
            {
                var stream = File.OpenRead(filePath);
                IFormFile file = new FormFile(stream, 0, stream.Length, "files", Path.GetFileName(filePath))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = filePath.Split('.')[1] == "jpg" ? "image/jpeg"
                        : filePath.Split('.')[1] == "png" ? "image/png"
                        : "image/bmp",
                };

                filesCollection.Add(file);
            }

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), filesCollection);

            return httpContext.Request.Form.Files;
        }
    }
}
