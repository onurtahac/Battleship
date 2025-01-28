using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipAPI.Domain.Entites;
using BattleshipAPI.Domain.Entities;

namespace BattleshipAPI.Domain.Interfaces
{
    public interface IShipRepository
    {
        Task AddAsync(Ships ship);  // Yeni bir gemi ekle
        Task<Ships> GetByIdAsync(int shipId);  // ShipId ile gemiyi getir
        Task<IEnumerable<Ships>> GetAllByUserIdAsync(int userId);  // Kullanıcıya ait tüm gemileri getir
        Task UpdateAsync(Ships ship);  // Gemi güncelle
        Task DeleteAsync(int shipId);  // Gemi sil
        Task SaveChangesAsync(int shipId);
        Task<Ships> GetByShipIdAsync(int  userId);
    }
}
