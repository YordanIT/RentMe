using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    public class TenantController : BaseController
    {
        public IActionResult Tenants()
        {
            return View();
        }

    }
}
