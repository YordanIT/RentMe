using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Remove()
        {
            return View();
        }

        public IActionResult Pay()
        {
            return View();
        }
    }
}
