using RentMe.Infrastructure.Data.Common;

namespace RentMe.Infrastructure.Data.Repositories
{
    public class ApplicationDbRepository : Repository, IApplicationDbRepository 
    {
        public ApplicationDbRepository(ApplicationDbContext context)
        {
            Context = context;
        }
    }
}
