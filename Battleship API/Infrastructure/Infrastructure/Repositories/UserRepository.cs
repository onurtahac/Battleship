using BattleshipAPI.Domain.Entites;
using BattleshipAPI.Domain.Interfaces;
using BattleshipAPI.Infrastructure.Parsistence;
using Microsoft.EntityFrameworkCore;

namespace BattleshipAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlDbContext _context;

        public UserRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<User> GetUserByEmailIdAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
