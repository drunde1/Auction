using Auction.DataAccess;
using Auction.DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auction.Infrastructure
{
    public class TangerineHostedService : IHostedService, IDisposable
    {
        private readonly Timer _timerClearer;
        private readonly Timer _timerCreater;
        private readonly Random _random;
        private string[] Places = { "Africa", "America", "India", "Russia", "Brazil", "England", "China", "Japan" };
        private readonly IServiceScopeFactory _scopeFactory;

        public TangerineHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _random = new Random();
            _timerCreater = new Timer(async (e) =>
            { await AddTangerine(); },
            null, TimeSpan.Zero,
               TimeSpan.FromMinutes(1));
            _timerClearer = new Timer(async (e) =>
            { await ClearTangerines(); },
            null, TimeSpan.Zero,
               TimeSpan.FromDays(1));
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        private async Task AddTangerine()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

                var tangerineEntity = new TangerineEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Tangerine",
                    Place = Places[_random.Next(0, Places.Length)],
                    Weight = (float)_random.Next(200, 1000) / 1000,
                    StartPrice = (decimal)_random.Next(1, 11) * 100,
                    IsActive = true,
                    ExpirationDate = DateTime.UtcNow.AddDays(5)
                };

                await dbContext.Tangerines.AddAsync(tangerineEntity);
                await dbContext.SaveChangesAsync();
            }
        }

        private async Task ClearTangerines()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

                dbContext.Tangerines.RemoveRange(dbContext.Tangerines.Where(t => t.ExpirationDate.Date < DateTime.UtcNow.Date));
                await dbContext.SaveChangesAsync();

            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timerCreater?.Change(Timeout.Infinite, 0);
            _timerClearer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timerCreater?.Dispose();
            _timerClearer?.Dispose();
        }
    }
}
