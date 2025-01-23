using BattleshipAPI.Domain.Entites;

namespace BattleshipAPI.Domain.Interfaces
{
    public interface IUserService
    {
        // Kullanıcıyı kaydetmek için
        Task<bool> RegisterAsync(string name, string surname, string email, string password);

        // Kullanıcı giriş işlemi için
        Task<string> LoginAsync(string email, string password);

        // Kullanıcı bilgilerini almak için
        Task<Users> GetUserByIdAsync(int userId);

        // Tüm kullanıcıları almak için
        Task<IEnumerable<Users>> GetAllUsersAsync();

        // Kullanıcı kendi bilgilerini güncelleyebilir
        Task<bool> UpdateUserAsync(int userId, string name, string surname, string email, string password);

        // Kullanıcı hesabını silebilir
        Task<bool> DeleteUserAsync(int userId);

    }
}
