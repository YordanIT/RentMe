using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Models;

namespace RentMe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdController : BaseController
    {
        [AllowAnonymous]
        public IActionResult ForRent()
        {
            var ads = new List<AdListViewModel>{new AdListViewModel
            {
                Description = "Modern topfloor apartment",
                Type = "Mesonet",
                Price = 1800,
                City = "Sofia",
                Address = "Downtown",
                Area = 250.00,
                Floor = 12,
                HasFurniture = "Yes",
                HasElevator = "Yes",
                HasParking = "Yes",
                Phone = "0889 150 266",
                Email = "pesho@mail.bg"

            },
            new AdListViewModel
            {
                Description = "Modern topfloor apartment",
                Type = "Mesonet",
                Price = 1800,
                City = "Sofia",
                Address = "Downtown",
                Area = 250.00,
                Floor = 12,
                HasFurniture = "Yes",
                HasElevator = "Yes",
                HasParking = "Yes",
                Phone = "0889 150 266",
                Email = "pesho@mail.bg"
            }};

            return View(ads);
        }
        
        public IActionResult AddAd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> DeleteAd()
        {
            return View();
        }
    }
}
