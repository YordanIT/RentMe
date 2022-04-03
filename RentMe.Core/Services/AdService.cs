using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Repositories;

namespace RentMe.Core.Services
{
    public class AdService : IAdService
    {
        private readonly IApplicationDbRepository repo;

        public AdService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public IEnumerable<AdListViewModel> GetAds()
        {
            throw new NotImplementedException();
        }

        public async Task AddAd(AdFormModel ad)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAd(AdListViewModel ad)
        {
            throw new NotImplementedException();
        }
    }
}
