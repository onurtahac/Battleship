﻿using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Application.Interfaces;
using BattleshipAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(LoginRequestDTO loginRequestDto, string role)
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
                new Claim(ClaimTypes.Role, role)
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

        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddDays(7)
            };
        }
    }
}
