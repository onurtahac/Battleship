using System;
using System.Threading.Tasks;
using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Application.Interfaces;
using BattleshipAPI.Domain.Entities;

namespace BattleshipAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserAppService _userAppService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(IUserAppService userAppService, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _userAppService = userAppService  ;
            _passwordHasher = passwordHasher  ;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> GetUserInfo(string userName)
        {
            try
            {
                var userDto = new UserDTO { Name = userName }; // UserDTO nesnesi oluşturuluyor

                var user = await _userAppService.GetByUserNameAsync(userDto);

                if (user == null)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Errors = new List<string> { "Kullanıcı bulunamadı." }
                    };
                }

                return new AuthResult
                {
                    Success = true,
                    UserName = userName // Kullanıcı bilgisi başarıyla dönüyor
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new List<string> { $"Bir hata oluştu: {ex.Message}" }
                };
            }
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            // Kullanıcıyı e-posta ile bul
            var user = await _userAppService.GetUserByEmailIdAsync(email);
            if (user == null)
                throw new UnauthorizedAccessException("Geçersiz e-posta veya şifre.");

            // Şifre kontrolü
            if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Geçersiz e-posta veya şifre.");
           
            var newlogin = new LoginRequestDTO 
            { 
                Password = password, 
                EmailAdress=email 
            };

            var token = _tokenService.GenerateToken(newlogin, user.role, user.UserId);

            return new AuthResult
            {
                Token = token,
                Success = true,
                RefreshToken = token,
            };
        }

        public async Task<AuthResult> RegisterAsync(RegisterDTO dto)
        {
            // Kullanıcının var olup olmadığını kontrol et
            var existingUser = await _userAppService.GetUserByEmailIdAsync(dto.EmailAddress);
            if (existingUser != null)
                throw new InvalidOperationException("Bu e-posta adresi zaten kayıtlı.");

            // Şifreyi hashle
            var hashedPassword = _passwordHasher.HashPassword(dto.PasswordHash);

            // Yeni kullanıcıyı oluştur
            var newUser = new UserDTO
            {
                Name = dto.FirstName,
                Surname = dto.LastName,
                Email = dto.EmailAddress,
                PasswordHash = hashedPassword,
                role = "User",
                CreatedAt = dto.CreatedAt,
                TotalPoints = 0,
            };

            var newLoginRequest = new LoginRequestDTO
            {
                EmailAdress = newUser.Email,
                Password = newUser.PasswordHash,
            };

            // Kullanıcıyı kaydet
            await _userAppService.AddUserAsync(newUser);
            var token = _tokenService.GenerateToken(newLoginRequest, newUser.role, newUser.UserId);
            var refreshToken= _tokenService.GenerateRefreshToken();
            return new AuthResult { Success = true, Token = token, RefreshToken = refreshToken.Token };
        }



    }
}
