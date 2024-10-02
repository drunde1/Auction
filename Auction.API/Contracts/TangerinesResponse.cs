namespace Auction.API.Contracts
{
    public record TangerinesResponse(
        Guid Id,
        string Name,
        string Place,
        float Weight,
        decimal StartPrice,
        bool IsActive,
        DateTime ExpirationDate);
}
