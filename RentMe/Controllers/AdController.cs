using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    public class AdController : Controller
    {
        public IActionResult ForRent()
        {
            return View();
        }

        public IActionResult ForSale()
        {
            return View();
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Remove()
        {
            return View();
        }
    }
}
