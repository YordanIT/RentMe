using Microsoft.AspNetCore.Mvc;
using RentMe.Infrastructure.Data;
using RentMe.Infrastructure.Data.Models;
using RentMe.Models;

namespace RentMe.Controllers
{
    public class BlogController : Controller
    {
        //"Views/Blog/Articles.cshtml"
        public IActionResult Articles()
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
    }
}
