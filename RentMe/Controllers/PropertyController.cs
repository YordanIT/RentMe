using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Identity;
using System.Security.Claims;

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            var applicationUser = await userManager.GetUserAsync(User);
            string userFirstName = applicationUser.FirstName;
            string userLastName = applicationUser.LastName; 

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

        public IActionResult Remove()
        {
            return View();
        }
    }
}
