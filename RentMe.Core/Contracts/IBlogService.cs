using RentMe.Core.Models;

namespace RentMe.Core.Contracts
{
    public interface IBlogService
    {
        IEnumerable<ArticleViewModel> GetArticles();
        Task AddArticle(ArticleFormModel article);
        Task DeleteArticle(ArticleViewModel article);
    }
}
