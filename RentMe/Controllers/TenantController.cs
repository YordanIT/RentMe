using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    public class TenantController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
