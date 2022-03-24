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

        public async Task AddArticle(ArticleFormModel model)
        {
            var article = await repo.All<Article>()
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.Title == model.Title);

            if (article != null)
            {
                throw new ArgumentException("Article with this title already exists or text is too long!");
            }

            article = new Article
            {
                Title = model.Title,
                Content = model.Content,
            };

            await repo.AddAsync(article);
            repo.SaveChanges();

        }

        public async Task DeleteArticle(ArticleEditModel model)
        {
            var article = await repo.All<Article>()
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (article == null)
            {
                throw new ArgumentException("Article does not exist!");
            }

            article.IsDeleted = true;

            repo.SaveChanges();
        }

        public IEnumerable<ArticleViewModel> GetArticles()
        {
            var articles = repo.All<Article>()
                .Where(a => a.IsDeleted == false)
                .ToList()
                .Select(a => new ArticleViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content
                }).ToList();

            return articles;
        }
    }
}
