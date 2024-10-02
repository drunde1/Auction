using Auction.Core.Models;
using Auction.DataAccess.Repositories;
using Auction.Infrastructure;

namespace Auction.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(
            IUsersRepository usersRepository, 
            IPasswordHasher passwordHasher, 
            IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.Get();
        }

        public async Task<Guid> Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var (user, error) = Core.Models.User.Create(
                Guid.NewGuid(),
                userName,
                email,
                hashedPassword);

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }

            return await _usersRepository.Create(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.HashedPassword);

            if (result ==  false) { return string.Empty; }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public async Task<Guid> UpdateUser(Guid id, string userName, string email, string password)
        {
            return await (_usersRepository.Update(id, userName, email, password));
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _usersRepository.Delete(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _usersRepository.GetByEmail(email);    
        }
    }
}
