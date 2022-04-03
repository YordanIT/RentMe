using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Contracts;
using RentMe.Core.Models;

namespace RentMe.Controllers
{
    public class BlogController : BaseController
    {

        private readonly IBlogService service;

        public BlogController(IBlogService _service)
        {
            service = _service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleFormModel article)
        {
            try
            {
                await service.AddArticle(article);
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = $"{ae.Message} Try again!" });
            }

            return RedirectToAction(nameof(Articles));
        }

        public async Task<IActionResult> Delete(ArticleViewModel article)
        {
            try
            {
                await service.DeleteArticle(article);
            }
            catch (ArgumentException ae)
            {
                return RedirectToAction("Message", "Home", new Message { Text = $"{ae.Message} Try again!" });
            }

            return RedirectToAction(nameof(Articles));
        }

        public IActionResult AddArticle()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Articles()
        {
            var articles = service.GetArticles();

            return View(articles);
        }
    }
}
