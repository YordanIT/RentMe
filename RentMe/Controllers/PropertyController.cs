using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Identity;

namespace RentMe.Controllers
{
    [Authorize]
    public class PropertyController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public PropertyController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
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

            var properties = new List<PropertyListViewModel>();

            return View(properties);
        }

        public IActionResult AddProperty()
        {

            return View();
        }

        public IActionResult AddPropertyType()
        {
            return View();
        }
    }
}
