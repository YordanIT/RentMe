using Microsoft.EntityFrameworkCore;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;

namespace RentMe.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IApplicationDbRepository repo;

        public BlogService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddArticle(ArticleViewModel model)
        {
            var article = await repo.All<Article>()
                .FirstOrDefaultAsync(a => a.Title == model.Title);

            if (article != null)
            {
                throw new ArgumentException("Article with this title already exists!");
            }

            article = new Article
            {
                Title = model.Title,
                Content = model.Content,
            };

            await repo.AddAsync(article);
            repo.SaveChanges();

        }
    }
}
