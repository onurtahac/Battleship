using BattleshipAPI.Application.DTOs;

 
namespace BattleshipAPI.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RegisterAsync(RegisterDTO dto);


    }
}
