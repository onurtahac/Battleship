using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Battleship_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var result = await _authService.LoginAsync(loginRequestDTO.EmailAdress, loginRequestDTO.Password);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }


        [HttpGet("getUserInfo")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userName = User.Identity.Name; // JWT token'dan kullanıcı adını alıyoruz
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User not authenticated");
            }

            // Burada, kullanıcı adı ile kullanıcı bilgisini getirebilirsiniz.
            var userInfo = await _authService.GetUserInfo(userName);  // userName üzerinden alınıyor

            if (userInfo == null || !userInfo.Success)
            {
                return NotFound("User not found");
            }

            // UserName bilgisi ekrana basılıyor
            return Ok(new { userName = userInfo.UserName, token = userInfo.Token, refreshToken = userInfo.RefreshToken });
        }

    }
}
