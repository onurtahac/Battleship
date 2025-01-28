using BattleshipAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Application.Interfaces
{
    public interface ICoordianteAppService
    {
        Task<Coordinates> AddAsync(Coordinates coordinate);  // Yeni bir koordinat ekle
        Task<Coordinates> GetByIdAsync(int coordinateId);  // Koordinat ID ile koordinat getir
        Task<IEnumerable<Coordinates>> GetByShipIdAsync(int shipId);  // ShipId ile koordinatları getir
        Task UpdateAsync(Coordinates coordinate);  // Koordinat güncelle
        Task DeleteAsync(int coordinateId);  // Koordinat sil
        Task SaveChangesAsync(int coordinateId);
    }
}
