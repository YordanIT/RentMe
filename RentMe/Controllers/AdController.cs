using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    public class AdController : BaseController
    {
        [AllowAnonymous]
        public IActionResult ForRent()
        {
            return View();
        }
        
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
