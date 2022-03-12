using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    public class PropertyController : Controller
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
