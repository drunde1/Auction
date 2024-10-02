using Auction.Core.Models;
using Auction.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AuctionDbContext _context;

        public UsersRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Get()
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            var users = userEntity
                .Select(u => User.Create(u.Id, u.UserName, u.Email, u.Password).User)
                .ToList();

            return users;
        }

        public async Task<Guid> Create(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.HashedPassword,
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string userName, string email, string password)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.UserName, u => userName)
                    .SetProperty(u => u.Email, u => email)
                    .SetProperty(u => u.Password, u => password));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            return User.Create(userEntity.Id, userEntity.UserName, userEntity.Email, userEntity.Password).User;
        }
    }
}
