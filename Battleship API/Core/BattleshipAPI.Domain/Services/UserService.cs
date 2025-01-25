using BattleshipAPI.Domain.Entites;
using BattleshipAPI.Domain.Interfaces;

namespace BattleshipAPI.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("User name and email cannot be empty.");
            }
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Email and password cannot be empty.");
            }
            return await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user.UserId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }
            return await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<User> GetUserByEmailIdAsync(string email)
        {
            return await _userRepository.GetUserByEmailIdAsync(email);
        }
    }
}



