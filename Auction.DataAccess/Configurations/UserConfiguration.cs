using Auction.Core.Models;
using Auction.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasMany(u => u.Tangerines)
                .WithMany(t => t.Users);

            builder.Property(u => u.UserName)
                .HasMaxLength(User.MAX_USERNAME_LENGTH)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(User.MAX_USERNAME_LENGTH)
                .IsRequired();

            builder.Property(u => u.Password)
                .IsRequired();
        }
    }
}
