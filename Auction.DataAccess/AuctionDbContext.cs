using Auction.DataAccess.Configurations;
using Auction.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options)
           : base(options)
        {
        }

        public DbSet<TangerineEntity> Tangerines { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TangerineConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
