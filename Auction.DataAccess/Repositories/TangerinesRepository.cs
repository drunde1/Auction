using Auction.Core.Models;
using Auction.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess.Repositories
{
    public class TangerinesRepository : ITangerinesRepository
    {
        private readonly AuctionDbContext _context;

        public TangerinesRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tangerine>> Get()
        {
            var tangerineEntities = await _context.Tangerines
                .AsNoTracking()
                .ToListAsync();

            var tangerines = tangerineEntities
                .Select(t => Tangerine.Create(t.Id, t.Name, t.Place, t.Weight, t.StartPrice, t.IsActive, t.ExpirationDate).Tangerine)
                .ToList();

            return tangerines;
        }

        public async Task<Guid> Create(Tangerine tangerine)
        {
            var tangerineEntity = new TangerineEntity
            {
                Id = tangerine.Id,
                Name = tangerine.Name,
                Place = tangerine.Place,
                Weight = tangerine.Weight,
                StartPrice = tangerine.StartPrice,
                IsActive = tangerine.IsActive,
                ExpirationDate = tangerine.ExpirationDate
            };

            await _context.Tangerines.AddAsync(tangerineEntity);
            await _context.SaveChangesAsync();

            return tangerineEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string name, string place, float weight, decimal startPrice, bool isActive, DateTime expirationDate)
        {
            await _context.Tangerines
                .Where(t => t.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(t => t.Name, t => name)
                    .SetProperty(t => t.Place, t => place)
                    .SetProperty(t => t.Weight, t => weight)
                    .SetProperty(t => t.StartPrice, t => startPrice)
                    .SetProperty(t => t.IsActive, t => isActive)
                    .SetProperty(t => t.ExpirationDate, t => expirationDate));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Tangerines
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<Guid> NewBet(Guid tangerineId, Guid userId, decimal newBet)
        {
            Guid previousUserBetId;
            var tangerine = await _context.Tangerines
                .Include(t => t.Users)
                .FirstOrDefaultAsync(t => t.Id == tangerineId && t.IsActive    )
                ?? throw new Exception("Tangerine has not been found");

            var user = await _context.Users
                .Include(u => u.Tangerines)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new Exception("User has not been found");

            if (newBet > tangerine.StartPrice)
            {
                tangerine.StartPrice = newBet;
                if (tangerine.Users.Any())
                {
                    previousUserBetId = tangerine.Users[0].Id;
                    tangerine.Users.RemoveAt(0);
                    var prevUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == previousUserBetId);
                    if ( prevUser != null)
                        prevUser.Tangerines.Remove(tangerine);
                }
                else
                    previousUserBetId = Guid.Empty;


                tangerine.Users.Add(user);
                await _context.SaveChangesAsync();

            }
            else { /*throw new Exception("You cant bet less than it already cost");*/ previousUserBetId = Guid.Empty; }


            return previousUserBetId;
        }
    }
}
