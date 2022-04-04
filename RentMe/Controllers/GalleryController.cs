using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Contracts;
using RentMe.Core.Models;

namespace RentMe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GalleryController : BaseController
    {
        private readonly IGalleryService service;

        public GalleryController(IGalleryService _service)
        {
            service = _service;
        }

        [AllowAnonymous]
        public IActionResult Images()
        {
            var images = service.GetImages();

            return View(images);
        }

        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ImageFormModel model)
        {
            var image = Request.Form.Files;

            try
            {
                await service.AddImage(image, model);
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = $"{ae.Message} Try again!" });
            }

            return RedirectToAction(nameof(Images));
        }

        public async Task<IActionResult> Delete(ImageEditModel image)
        {
            try
            {
                await service.DeleteImage(image);
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = $"{ae.Message} Try again!" });
            }

            return RedirectToAction(nameof(Images));
        }
    }
}
