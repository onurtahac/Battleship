using BattleshipAPI.Application.Interfaces;
using BattleshipAPI.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Battleship_API.Controllers
{
    [Authorize] // Token doğrulaması yapılması için ekleniyor
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                // Kullanıcı kimliğini al
                var userClaims = User.Identity as ClaimsIdentity;
                var userName = userClaims?.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(userName))
                {
                    return Unauthorized(new { Message = "Token geçerli değil veya kullanıcı adı bulunamadı." });
                }

                // Kullanıcı bilgilerini al
                var userInfo = await _userAppService.GetUserInfo(userName);

                if (userInfo == "Kullanıcı bulunamadı.")
                {
                    return NotFound(new { Message = "Kullanıcı sistemde kayıtlı değil." });
                }

                return Ok(new { UserName = userInfo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Bir hata oluştu: {ex.Message}" });
            }
        }
    }
}
