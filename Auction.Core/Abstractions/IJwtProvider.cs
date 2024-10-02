using Auction.Core.Models;
using System.Security.Claims;

namespace Auction.Infrastructure
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
        ClaimsPrincipal ValidateToken(string token);
    }
}