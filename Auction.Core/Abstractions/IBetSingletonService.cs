
namespace Auction.Application.Services
{
    public interface IBetSingletonService
    {
        Task NewBet(Guid tangerineId);
    }
}