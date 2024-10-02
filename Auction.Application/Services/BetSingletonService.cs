using Auction.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Auction.Application.Services
{
    public class BetSingletonService : IBetSingletonService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Dictionary<Guid, Timer> tangerineTimer;

        public BetSingletonService(IServiceScopeFactory scopeFactory)
        {
            tangerineTimer = new Dictionary<Guid, Timer>();
            _scopeFactory = scopeFactory;
        }

        public async Task NewBet(Guid tangerineId)
        {
            if (tangerineTimer.ContainsKey(tangerineId))
            {
                await tangerineTimer[tangerineId].DisposeAsync();
                tangerineTimer[tangerineId] = new Timer(async (e) =>
                {
                    await SetPassive(tangerineId);
                }, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));
            }
            else 
            {
                tangerineTimer.Add(tangerineId, new Timer(async (e) =>
                {
                    await SetPassive(tangerineId);
                }, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
            }
        }

        private async Task SetPassive(Guid tangerineId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

                await dbContext.Tangerines
                    .Where(t => t.Id == tangerineId)
                    .ExecuteUpdateAsync(t => t
                    .SetProperty(t => t.IsActive, t => false));

                await dbContext.SaveChangesAsync();
            }
            await tangerineTimer[tangerineId].DisposeAsync();
            tangerineTimer.Remove(tangerineId);
        }
    }
}
