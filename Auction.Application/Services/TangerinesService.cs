using Auction.Core.Models;
using Auction.DataAccess.Repositories;
using Auction.Infrastructure;

namespace Auction.Application.Services
{
    public class TangerinesService : ITangerinesService
    {
        private readonly ITangerinesRepository _tangerinesRepository;
        private readonly IJwtProvider _jwtProvider;

        public TangerinesService(ITangerinesRepository tangerinesRepository, IJwtProvider jwtProvider)
        {
            _tangerinesRepository = tangerinesRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<List<Tangerine>> GetAllTangerines()
        {
            return await _tangerinesRepository.Get();
        }

        public async Task<Guid> CreateTangerine(Tangerine tangerine)
        {
            return await _tangerinesRepository.Create(tangerine);
        }

        public async Task<Guid> UpdateTangerine(Guid id, string name, string place, float weight, decimal price, bool isActive, DateTime expirationDate)
        {
            return await _tangerinesRepository.Update(id, name, place, weight, price,isActive, expirationDate);
        }

        public async Task<Guid> DeleteTangerine(Guid id)
        {
            return await _tangerinesRepository.Delete(id);
        }

        public async Task<Guid> NewBet(Guid tangerineId, string token, decimal newBet)
        {
            var ID = _jwtProvider.ValidateToken(token)?.FindFirst("userId")?.Value!;
            var userId = new Guid(ID);
            return await _tangerinesRepository.NewBet(tangerineId, userId , newBet);
        }
    }
}
