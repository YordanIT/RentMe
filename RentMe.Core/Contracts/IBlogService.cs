using RentMe.Core.Models;

namespace RentMe.Core.Contracts
{
    public interface IBlogService
    {
        Task AddArticle(ArticleFormModel article);
        Task DeleteArticle(ArticleViewModel article);
        IEnumerable<ArticleViewModel> GetArticles();
    }
}
