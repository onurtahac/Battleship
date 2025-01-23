using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Domain.Entites;

namespace BattleshipAPI.Application.Interfaces
{
    public  interface IUserAppService
    {

        // Kullanıcıyı kaydetmek için
        Task<bool> RegisterAsync(string name, string surname, string email, string password);

        // Kullanıcı giriş işlemi için
        Task<string> LoginAsync(string email, string password);

        // Kullanıcı bilgilerini almak için
        Task<DTOs.Users> GetUserByIdAsync(int userId);

        // Tüm kullanıcıları almak için
        Task<IEnumerable<DTOs.Users>> GetAllUsersAsync();

        // Kullanıcı kendi bilgilerini güncelleyebilir
        Task<bool> UpdateUserAsync(int userId, string name, string surname, string email, string password);

        // Kullanıcı hesabını silebilir
        Task<bool> DeleteUserAsync(int userId);

       

    }
}
