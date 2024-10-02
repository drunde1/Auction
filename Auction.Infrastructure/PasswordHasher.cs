namespace Auction.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string hasedPassword) => 
            BCrypt.Net.BCrypt.EnhancedVerify(password, hasedPassword);
    }
}
