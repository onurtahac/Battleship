using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipAPI.Domain.Entities;


namespace BattleshipAPI.Domain.Interfaces
{
    public interface ICoordinateRepository
    {
        Task<Coordinates> AddAsync(Coordinates coordinate);  // Yeni bir koordinat ekle
        Task<Coordinates> GetByIdAsync(int coordinateId);  // Koordinat ID ile koordinat getir
        Task<IEnumerable<Coordinates>> GetByShipIdAsync(int shipId);  // ShipId ile koordinatları getir
        Task UpdateAsync(Coordinates coordinate);  // Koordinat güncelle
        Task DeleteAsync(int coordinateId);  // Koordinat sil
        Task SaveChangesAsync(int coordinateId);
    }
    
}
