using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Application.Interfaces;
using BattleshipAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(LoginRequestDTO loginRequestDto, string role, int userId)
    {
        if (loginRequestDto == null)
        {
            throw new ArgumentNullException(nameof(loginRequestDto), "Login request cannot be null.");
        }

        if (string.IsNullOrEmpty(loginRequestDto.EmailAdress))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(loginRequestDto.EmailAdress));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = _configuration["Jwt:Key"];

        if (string.IsNullOrEmpty(key))
        {
            throw new InvalidOperationException("JWT key is not configured.");
        }

        var claims = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, loginRequestDto.EmailAdress),
            new Claim(ClaimTypes.Role, role),
            new Claim("userId", userId.ToString())   // User ID'yi de ekledik
        });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var userId = jwtToken?.Payload?.Claims
            .FirstOrDefault(c => c.Type == "userId")?.Value;

        return userId;
    }

    public RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            Expiration = DateTime.UtcNow.AddDays(7)
        };
    }
}
