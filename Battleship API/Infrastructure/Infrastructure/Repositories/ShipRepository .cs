using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Domain.Entites;
using BattleshipAPI.Domain.Entities;
using BattleshipAPI.Domain.Interfaces;
using BattleshipAPI.Infrastructure.Parsistence;
using Microsoft.EntityFrameworkCore;

namespace BattleshipAPI.Infrastructure.Repositories
{
    public class ShipRepository : IShipRepository
    {
        private readonly SqlDbContext _context;

        public ShipRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ShipsDTO shipDto)
        {
            // DTO'dan Entity'e dönüştürme
            var ship = new Ships
            {
                Length = shipDto.Length,
                IsVertical = shipDto.IsVertical,
                UserId = shipDto.UserId
            };

            // Entity ekleme işlemi
            _context.Ships.Add(ship);
            await _context.SaveChangesAsync();
        }


        public Task AddAsync(Domain.Entities.Ships ship)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int shipId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Ships>> GetAllByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Ships> GetByIdAsync(int shipId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync(int ShipId)
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Domain.Entities.Ships ship)
        {
            throw new NotImplementedException();
        }

        public Task<Ships> GetByShipIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

    }
}
