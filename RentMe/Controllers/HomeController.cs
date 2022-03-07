using Microsoft.AspNetCore.Mvc;
using RentMe.Models;
using System.Diagnostics;

namespace RentMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
           _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ForRent()
        {
            return View("Views/Ads/ForRent.cshtml");
        }

        public IActionResult ForSale()
        {
            return View("Views/Ads/ForSale.cshtml");
        }

        public IActionResult Blog()
        {
            return View("Views/Blog/Articles.cshtml");
        }

        public IActionResult Add()
        {
            return View("Views/Blog/Add.cshtml");
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}