using RentMe.Core.Models;

namespace RentMe.Core.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserListViewModel> GetUsers();
        Task SetAsAdmin(string id);
        Task SetAsLandlord(string id);
    }
}
