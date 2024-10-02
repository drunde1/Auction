using Auction.Core.Models;

namespace Auction.DataAccess.Repositories
{
    public interface ITangerinesRepository
    {
        Task<Guid> Create(Tangerine tangerine);
        Task<Guid> Delete(Guid id);
        Task<List<Tangerine>> Get();
        Task<Guid> Update(Guid id, string name, string place, float weight, decimal startPrice, bool isActive, DateTime expirationDate);
        Task<Guid> NewBet(Guid tangerineId, Guid userId, decimal newBet);
    }
}