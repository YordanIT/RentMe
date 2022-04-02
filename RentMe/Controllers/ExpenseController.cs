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

        public IActionResult Expenses(TenantViewModel tenant)
        {
            var expenses = service.GetExpenses(tenant);
            
            if (!expenses.Any())
            {
                return RedirectToAction(nameof(AddInitialExpense), tenant);
            }

            return View(expenses);
        }

        public IActionResult AddExpense(ExpenseListViewModel model)
        {
            var tenants = service.GetTenant(model);
            ViewBag.Tenants = tenants;

            return View();
        }

        public IActionResult AddInitialExpense(TenantViewModel model)
        {
            var tenants = service.GetTenant(model);
            ViewBag.Tenants = tenants;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(ExpenseFormModel expense)
        {
            try
            {
                await service.AddExpense(expense);
            }
            catch (ArgumentException)
            {
                return BadRequest("Unexpected error!");
            }

            return Ok("Successfully added expenses!");
        }

        public async Task<IActionResult> DeleteExpense(ExpenseListViewModel expense)
        {
            try
            {
                await service.DeleteExpense(expense);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

            return Ok("Successfully delete expenses!");
        }

        public async Task<IActionResult> EditExpense(ExpenseListViewModel expense)
        {
            try
            {
                await service.EditExpense(expense);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

            return Ok("Successfully mark as paid!");
        }
    }
}
