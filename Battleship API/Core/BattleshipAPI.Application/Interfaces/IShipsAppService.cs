using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Domain.Entities;

namespace BattleshipAPI.Application.Interfaces
{
    internal interface IShipsAppService
    {
        Task AddAsync(Ships ship);  // Yeni bir gemi ekle
        Task<Ships> GetByShipIdAsync(int shipId);  // ShipId ile gemiyi getir
        Task<IEnumerable<Ships>> GetAllByUserIdAsync(int userId);  // Kullanıcıya ait tüm gemileri getir
        Task UpdateAsync(Ships ship);  // Gemi güncelle
        Task DeleteAsync(int shipId);  // Gemi sil

    }
}
