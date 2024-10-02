using Auction.Core.Models;

namespace Auction.Application.Services
{
    public interface ITangerinesService
    {
        Task<Guid> CreateTangerine(Tangerine tangerine);
        Task<Guid> DeleteTangerine(Guid id);
        Task<List<Tangerine>> GetAllTangerines();
        Task<Guid> UpdateTangerine(Guid id, string name, string place, float weight, decimal price, bool isActive, DateTime expirationDate);
        Task<Guid> NewBet(Guid tangerineId, string token, decimal newBet);
    }
}