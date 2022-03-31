using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Contracts;
using RentMe.Core.Models;

namespace RentMe.Controllers
{
    [Authorize]
    public class ExpenseController : BaseController
    {
        private readonly IExpenseService service;

        public ExpenseController(IExpenseService _service)
        {
            service = _service;
        }

        public IActionResult Expenses(TenantViewModel model)
        {
            var expenses = service.GetExpenses(model);

            if (expenses == null)
            {
                return RedirectToAction(nameof(AddExpense), model);
            }

            return View(expenses);
        }

        public IActionResult AddExpense(TenantViewModel model)
        {
            //To Do : add viewbag tenant
            //var tenant = service.GetTenant(model);
            //ViewBag.Tenant = tenant;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(ExpenseFormModel tenant)
        {
            try
            {
                await service.AddExpense(tenant);
            }
            catch (ArgumentException)
            {
                return BadRequest("Unexpected error!");
            }

            return RedirectToAction(nameof(Expenses));
        }

        public async Task<IActionResult> DeleteExpense(ExpenseListViewModel tenant)
        {
            try
            {
                await service.DeleteExpense(tenant);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

            return RedirectToAction(nameof(Expenses));
        }
    }
}
