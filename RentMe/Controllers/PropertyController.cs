using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Identity;

namespace RentMe.Controllers
{
    [Authorize]
    public class PropertyController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPropertyService service;

        public PropertyController(UserManager<ApplicationUser> _userManager, IPropertyService _service)
        {
            userManager = _userManager;
            service = _service;
        }

        public async Task<IActionResult> Properties()
        {
            var applicationUser = await userManager.GetUserAsync(User);
            string? userFirstName = applicationUser.FirstName;
            string? userLastName = applicationUser.LastName; 

            if (userFirstName == null || userLastName == null)
            {
                return Redirect("/User/RegisterUserNames");
            }

            var properties = service.GetProperties(applicationUser);
         
            return View(properties);
        }

        public IActionResult AddProperty()
        {
            var propertyTypes = service.GetPropertyTypes();

            ViewBag.PropertyTypes = propertyTypes;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProperty(PropertyFormModel property)
        {
            try
            {
                await service.AddProperty(property, await userManager.GetUserAsync(User));
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = $"{ae.Message} Try again!" });
            }

            return RedirectToAction(nameof(Properties));
        }

        public IActionResult AddPropertyType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropertyType(PropertyTypeFormModel propertyType)
        {
            try
            {
                await service.AddPropertyType(propertyType);
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = $"{ae.Message} Try again!" });
            }

            return RedirectToAction(nameof(Properties));
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PropertyListViewModel property)
        {
            try
            {
                await service.DeleteProperty(property);
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = ae.Message});
            }

            return RedirectToAction(nameof(Properties));
        }
    }
}
