using Microsoft.AspNetCore.Http;
using RentMe.Core.Models;

namespace RentMe.Core.Contracts
{
    public interface IGalleryService
    {
        Task AddImage(IFormFileCollection image, ImageFormModel model);
        Task DeleteImage(ImageEditModel model);
        IEnumerable<ImageViewModel> GetImages();
    }
}
