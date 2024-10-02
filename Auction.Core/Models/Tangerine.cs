namespace Auction.Core.Models
{
    public class Tangerine
    {
        public const int MAX_NAME_LENGTH = 50;

        private Tangerine(Guid id, string name, string place, float weight, decimal startPrice, bool isActive, DateTime expirationDate)
        {
            Id = id;
            Name = name;
            Place = place;
            Weight = weight;
            StartPrice = startPrice;
            IsActive = isActive;
            ExpirationDate = expirationDate;

        }

        public Guid Id { get; }

        public string Name { get; } = string.Empty;

        public string Place { get; } = string.Empty;

        public float Weight { get; }

        public decimal StartPrice { get; }

        public bool IsActive { get; }

        public DateTime ExpirationDate { get; } = DateTime.UtcNow.AddDays(10);

        public static (Tangerine Tangerine, string Error) Create(
            Guid id, string name, string place, float weight, decimal startPrice, bool isActive, DateTime expirationDate)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            {
                error = "Name cant be empty or > 50";
            }

            //if (expirationDate < DateTime.UtcNow)
            //{
            //    error = "Incorect ExpirationDate value";
            //}

            var tangerine = new Tangerine(id, name, place, weight, startPrice, isActive, expirationDate);

            return (tangerine, error);
        }
    }
}
