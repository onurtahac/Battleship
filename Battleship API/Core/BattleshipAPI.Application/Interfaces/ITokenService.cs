using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(LoginRequestDTO loginRequestDto, string role);
        RefreshToken GenerateRefreshToken();

    }
}
