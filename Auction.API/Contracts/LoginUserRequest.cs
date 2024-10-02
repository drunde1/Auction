namespace Auction.API.Contracts
{
    public record LoginUserRequest(
        string Email,
        string Password);
}
