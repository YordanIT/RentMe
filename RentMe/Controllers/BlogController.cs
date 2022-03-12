using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Remove()
        {
            return View();
        }
    }
}
