namespace Auction.API.Contracts
{
    public record UsersResponse(
        Guid Id,
        string UserName,
        string Email,
        string Password);
}
