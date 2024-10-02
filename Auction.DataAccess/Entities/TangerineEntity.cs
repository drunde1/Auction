namespace Auction.DataAccess.Entities
{
    public class TangerineEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Place { get; set; } = string.Empty;

        public float Weight { get; set; }

        public decimal StartPrice { get; set; }

        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(10);

        public bool IsActive { get; set; } = true;

        public List<UserEntity> Users { get; set; } = [];
    }
}
