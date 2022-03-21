using RentMe.Core.Models;

namespace RentMe.Core.Contracts
{
    public interface IBlogService
    {
        Task AddArticle(ArticleViewModel article);
    }
}
