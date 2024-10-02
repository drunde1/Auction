namespace Auction.API.Contracts
{
    public record UsersRequest(
        string UserName,
        string Email,
        string Password);
}
