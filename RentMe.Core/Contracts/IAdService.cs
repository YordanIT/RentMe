using RentMe.Core.Models;

namespace RentMe.Core.Contracts
{
    public interface IAdService
    {
        IEnumerable<AdListViewModel> GetAds();
        Task AddAd(AdFormModel ad);
        Task DeleteAd(AdListViewModel ad);
    }
}
