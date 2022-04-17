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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExpense(ExpenseFormModel expense)
        {
            try
            {
                await service.AddExpense(expense);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Message", "Home", new Message { Text = "Unexpected error! Try again!" });
            }

            return RedirectToAction("Message", "Home", new Message { Text = "Successfully added expenses! Enjoy your stay at RentMe" });
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExpense(ExpenseListViewModel expense)
        {
            try
            {
                await service.DeleteExpense(expense);
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = $"{ae.Message} Try again!" });
            }

            return RedirectToAction("Message", "Home", new Message { Text = "Successfully deleted expenses! Enjoy your stay at RentMe" });
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExpense(ExpenseListViewModel expense)
        {
            try
            {
                await service.EditExpense(expense);
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = $"{ae.Message} Try again!" });
            }

            return RedirectToAction("Message", "Home", new Message { Text = "Successfully mark as paid! Enjoy your stay at RentMe" });
        }
    }
}
