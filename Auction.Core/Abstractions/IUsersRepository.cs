using Auction.Core.Models;

namespace Auction.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<Guid> Create(User user);
        Task<Guid> Delete(Guid id);
        Task<List<User>> Get();
        Task<Guid> Update(Guid id, string userName, string email, string password);
        Task<User> GetByEmail(string email);
    }
}