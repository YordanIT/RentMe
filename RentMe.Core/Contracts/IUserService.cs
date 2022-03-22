using Microsoft.AspNetCore.Identity;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Identity;

namespace RentMe.Core.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserListViewModel> GetUsers();
        Task<UserEditViewModel> SetAsAdmin(string id);
    }
}
