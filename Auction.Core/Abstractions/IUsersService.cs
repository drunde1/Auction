using Auction.Core.Models;

namespace Auction.Application.Services
{
    public interface IUsersService
    {
        Task<Guid> Register(string userName, string email, string password);
        Task<Guid> DeleteUser(Guid id);
        Task<List<User>> GetAllUsers();
        Task<Guid> UpdateUser(Guid id, string userName, string email, string password);
        Task<User> GetUserByEmail(string email);
        Task<string> Login(string email, string password);
    }
}