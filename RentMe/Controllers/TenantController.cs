using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Contracts;
using RentMe.Core.Models;

namespace RentMe.Controllers
{
    public class TenantController : BaseController
    {
        private readonly ITenantService service;

        public TenantController(ITenantService _service)
        {
            service = _service;
        }

        public IActionResult TenantInfo(PropertyListViewModel model)
        {
            var tenant = service.GetTenant(model);

            if (tenant == null)
            {
                return RedirectToAction(nameof(AddTenant), model);
            }
           
            return View(tenant);
        }

        public IActionResult AddTenant(PropertyListViewModel model)
        {
            var properties = service.GetProperties(model);
            ViewBag.Properties = properties;
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenant(TenantFormModel tenant)
        {
            try
            {
                await service.AddTenant(tenant);
            }
            catch (ArgumentException)
            {
                return BadRequest("Unexpected error!");
            }

            return Redirect("~/Property/Properties");
        }
    }
}
