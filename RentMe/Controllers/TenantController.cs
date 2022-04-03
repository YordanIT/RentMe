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
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = $"{ae.Message} Try again!" });
            }

            return Redirect("~/Property/Properties");
        }

        public async Task<IActionResult> RemoveTenant(TenantViewModel tenant)
        {
            try
            {
                await service.RemoveTenant(tenant);
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = ae.Message });
            }

            return Redirect("~/Property/Properties");
        }
    }
}
