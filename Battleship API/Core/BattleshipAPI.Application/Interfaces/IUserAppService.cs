    using BattleshipAPI.Application.DTOs;
    using BattleshipAPI.Domain.Entites;

    namespace BattleshipAPI.Application.Interfaces
    {
        public  interface IUserAppService
        {
            Task<bool> AddUserAsync(UserDTO user);
            Task<UserDTO> GetUserByEmailAndPasswordAsync(string email, string password);
            Task<UserDTO> GetUserByIdAsync(int userId);
            Task<IEnumerable<UserDTO>> GetAllUsersAsync();
            Task<bool> UpdateUserAsync(UserDTO user);
            Task<bool> DeleteUserAsync(int userId);
            Task<bool> RegisterAsync(string name, string surname, string email, string password);
            Task<string> LoginAsync(string email, string password);
            Task<UserDTO> GetUserByEmailIdAsync(string email);
            Task<UserDTO> GetByUserNameAsync(string name);
            Task<string> GetUserInfo(string name);

        }
    }
