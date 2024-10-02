namespace Auction.API.Contracts
{
    public record TangerinesRequest(
        string Name,
        string Place,
        float Weight,
        decimal StartPrice,
        bool IsActive,
        DateTime ExpirationDate);
}
