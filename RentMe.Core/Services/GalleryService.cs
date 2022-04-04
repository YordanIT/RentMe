using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;

namespace RentMe.Core.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IApplicationDbRepository repo;

        public GalleryService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public IEnumerable<ImageViewModel> GetImages()
        {
            var images = repo.All<Image>()
                .Where(i => i.IsDeleted == false)
                .OrderByDescending(i => i.Id)
                .Select(i => new ImageViewModel
                {
                    Description = i.Description,
                    Id = i.Id,
                    DataUrl =
                    string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(i.Data))
                })
                .ToList();

            return images;
        }

        public async Task AddImage(IFormFileCollection images, ImageFormModel model)
        {
            if (!images.Any())
            {
                throw new ArgumentException("There is no image uploaded!");
            }

            foreach (var file in images)
            {
                var image = new Image
                {
                    Title = file.FileName,
                    Description = model.Description
                };

                var ms = new MemoryStream();
                file.CopyTo(ms);
                image.Data = ms.ToArray();

                ms.Close();
                ms.Dispose();

                await repo.AddAsync(image);
                await repo.SaveChangesAsync();
            }
        }

        public async Task DeleteImage(ImageEditModel model)
        {
            var image = await repo.All<Image>()
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (image == null)
            {
                throw new ArgumentException("Image does not exist!");
            }

            image.IsDeleted = true;

            repo.SaveChanges();
        }
    }
}
