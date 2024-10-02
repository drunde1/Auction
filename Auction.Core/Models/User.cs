namespace Auction.Core.Models
{
    public class User
    {
        public const int MAX_USERNAME_LENGTH = 20;
        public const int MIN_PASSWORD_LENGTH = 8;

        private User(Guid id, string userName, string email, string hashedPassword)
        {
            Id = id;
            UserName = userName;
            Email = email;
            HashedPassword = hashedPassword;
        }

        public Guid Id { get; }

        public string UserName { get; } = string.Empty;

        public string Email { get; } = string.Empty;

        public string HashedPassword { get; } = string.Empty;

        public static (User User, string Error) Create(Guid id, string userName, string email, string hashedPassword)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(userName) || userName.Length > MAX_USERNAME_LENGTH)
            {
                error = "Name cant be empty or > 50";
            }

            if (string.IsNullOrEmpty(email) || email.Length > MAX_USERNAME_LENGTH)
            {
                error = "Email cant be empty or > 50";
            }

            if (hashedPassword.Length < MIN_PASSWORD_LENGTH)
            {
                error = "Password cant be less than 8 symbols length";
            }

            var user = new User(id, userName, email, hashedPassword);

            return (user, error);
        }
    }
}
