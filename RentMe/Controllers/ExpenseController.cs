using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    [Authorize]
    public class ExpenseController : BaseController
    {
        public IActionResult Expenses()
        {
            return View();
        }

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
