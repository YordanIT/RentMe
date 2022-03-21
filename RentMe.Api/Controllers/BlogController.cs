using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Contracts;
using RentMe.Core.Models;

namespace RentMe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService service;

        public BlogController(IBlogService _service)
        {
            service = _service;
        }

        /// <summary>
        /// Adding an article to blog section
        /// </summary>
        /// <param name="article">New Article</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddArticle(ArticleViewModel article)
        {
            try
            {
                await service.AddArticle(article);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

            return Ok();
        }
    }
}
