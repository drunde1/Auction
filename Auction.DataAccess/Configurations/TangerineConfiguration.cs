using Auction.Core.Models;
using Auction.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.DataAccess.Configurations
{
    public class TangerineConfiguration : IEntityTypeConfiguration<TangerineEntity>
    {
        public void Configure(EntityTypeBuilder<TangerineEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .HasMany(t => t.Users)
                .WithMany(u => u.Tangerines);

            builder.Property(t => t.Name)
                .HasMaxLength(Tangerine.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(t => t.Place)
                .HasMaxLength(Tangerine.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(t => t.Weight)
               .IsRequired();

            builder.Property(t => t.StartPrice)
               .IsRequired();

            builder.Property(t => t.IsActive)
                .IsRequired();
        }
    }
}
