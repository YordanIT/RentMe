using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    public class TenantController : BaseController
    {
        public IActionResult Register()
        {
            return View();
        }

    }
}
