using BattleshipAPI.Domain.Entities;
using BattleshipAPI.Domain.Interfaces;
using BattleshipAPI.Infrastructure.Parsistence;
using Microsoft.EntityFrameworkCore;


namespace BattleshipAPI.Infrastructure.Repositories
{
    public class CoordinateRepository : ICoordinateRepository
    {
        private readonly SqlDbContext _context;

        public CoordinateRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<Coordinates> AddAsync(Coordinates coordinate)
        {
            await _context.Coordinates.AddAsync(coordinate);
            await _context.SaveChangesAsync();
            return coordinate; // Eklenen koordinatı döndürüyoruz
        }

        public async Task<Coordinates> GetByIdAsync(int coordinateId)
        {
            return await _context.Coordinates
                .FirstOrDefaultAsync(c => c.CoordinateId == coordinateId); // Koordinat ID ile getir
        }

        public async Task<IEnumerable<Coordinates>> GetByShipIdAsync(int shipId)
        {
            return await _context.Coordinates
                .Where(c => c.ShipId == shipId) // ShipId ile koordinatları getir
                .ToListAsync();
        }

        public async Task UpdateAsync(Coordinates coordinate)
        {
            _context.Coordinates.Update(coordinate);
            await _context.SaveChangesAsync(); // Güncellenen koordinatı kaydet
        }

        public async Task DeleteAsync(int coordinateId)
        {
            var coordinate = await GetByIdAsync(coordinateId);
            if (coordinate != null)
            {
                _context.Coordinates.Remove(coordinate); // Koordinatı sil
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync(int coordinateId)
        {
            await _context.SaveChangesAsync(); // Değişiklikleri kaydet
        }
    }
}
