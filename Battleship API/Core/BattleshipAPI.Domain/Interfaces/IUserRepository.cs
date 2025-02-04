using BattleshipAPI.Domain.Entites;

namespace BattleshipAPI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<User> GetUserByEmailIdAsync(string email);
        Task<User?> GetByUserNameAsync(string userName);
    }
}
