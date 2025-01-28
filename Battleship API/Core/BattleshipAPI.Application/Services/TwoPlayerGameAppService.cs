using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Application.Interfaces;
using BattleshipAPI.Domain.Entities;
using BattleshipAPI.Domain.Interfaces;

namespace BattleshipAPI.Application.Services
{
    public class TwoPlayerGameAppService : ITwoPlayerGameAppService
    {
        private readonly IShipRepository _shipRepository;
        private readonly ICoordinateRepository _coordinateRepository;
        private readonly ITwoPlayerGameService _twoPlayerGameService;

        public TwoPlayerGameAppService(
            IShipRepository shipRepository,
            ICoordinateRepository coordinateRepository,
            ITwoPlayerGameService twoPlayerGameService)
        {
            _shipRepository = shipRepository;
            _coordinateRepository = coordinateRepository;
            _twoPlayerGameService = twoPlayerGameService;
        }

        // Ships yerleştir
        public async Task<bool> PlaceShipsAsync(int playerId, List<Ships> ships)
        {
            foreach (var ship in ships)
            {
                var newShip = new Ships
                {
                    UserId = playerId,
                    Length = ship.Length,
                    IsVertical = ship.IsVertical
                };

                await _shipRepository.AddAsync(newShip);
                await _shipRepository.SaveChangesAsync(newShip.ShipId);

                var coordinates = GetShipCoordinates(newShip, ship.Length, ship.IsVertical);
                foreach (var coord in coordinates)
                {
                    // DTO'dan Domain Entity'ye dönüşüm
                    var newCoordinate = new Coordinates
                    {
                        ShipId = newShip.ShipId,
                        Row = coord.Row,
                        Col = coord.Col
                    };

                    await _coordinateRepository.AddAsync(newCoordinate); // Doğru türde nesne gönderiliyor
                }

                await _coordinateRepository.SaveChangesAsync(ship.ShipId);
            }

            return true;
        }

        // Yeni bir oyun başlat
        public async Task<int> StartGameAsync(int player1Id, int player2Id)
        {
            return await _twoPlayerGameService.StartGameAsync(player1Id, player2Id);
        }

        // Oyuncunun hamlesini yap
        public async Task<bool> MakeMoveAsync(int gameId, int playerId, int chosenNumber)
        {
            return await _twoPlayerGameService.MakeMoveAsync(gameId, playerId, chosenNumber);
        }

        // Oyun durumunu al
        public async Task<TwoPlayerGame> GetGameStatusAsync(int gameId)
        {
            // Domain entity'sini al
            var game = await _twoPlayerGameService.GetGameStatusAsync(gameId);

            // DTO'ya dönüştür
            var gameDto = new TwoPlayerGame
            {
                GameId = game.GameId,
                Player1Id = game.Player1Id,
                Player2Id = game.Player2Id,
                GameResult = game.GameResult,
                CreatedAt = game.CreatedAt
            };

            return gameDto;
        }


        public async Task<IEnumerable<TwoPlayerGame>> GetAllGamesAsync()
        {
            // Domain entity koleksiyonunu al
            var games = await _twoPlayerGameService.GetAllGamesAsync();

            // DTO koleksiyonuna dönüştür
            var gameDtos = games.Select(game => new TwoPlayerGame
            {
                GameId = game.GameId,
                Player1Id = game.Player1Id,
                Player2Id = game.Player2Id,
                GameResult = game.GameResult,
                CreatedAt = game.CreatedAt
            });

            return gameDtos;
        }


        // Oyunu sonuçlandır
        public async Task<bool> EndGameAsync(int gameId, string result)
        {
            return await _twoPlayerGameService.EndGameAsync(gameId, result);
        }

        // Oyuncunun hamlelerini getir
        public async Task<IEnumerable<int>> GetPlayerMovesAsync(int gameId, int playerId)
        {
            return await _twoPlayerGameService.GetPlayerMovesAsync(gameId, playerId);
        }

        // Tankların yerleştirildiği pozisyonları getir
        public async Task<IEnumerable<int>> GetTankPositionsAsync(int gameId, int playerId)
        {
            return await _twoPlayerGameService.GetTankPositionsAsync(gameId, playerId);
        }

        // Gemi koordinatlarını hesapla
        private List<CoordinatesDTOs> GetShipCoordinates(Ships ship, int length, bool isVertical)
        {
            var coordinates = new List<CoordinatesDTOs>();
            int startRow = 3;  // Örnek: dinamik alabilirsiniz
            int startCol = 5;  // Örnek: dinamik alabilirsiniz

            for (int i = 0; i < length; i++)
            {
                if (isVertical)
                {
                    coordinates.Add(new CoordinatesDTOs { Row = (char)(startRow + i), Col = startCol });
                }
                else
                {
                    coordinates.Add(new CoordinatesDTOs { Row = (char)startRow, Col = startCol + i });
                }
            }

            return coordinates;
        }

        // Check if a hit is successful
        public async Task<bool> CheckHitAsync(int row, int col, int gameId, int userId)
        {
            // Kullanıcıya ait tüm gemileri al
            var ships = await _shipRepository.GetAllByUserIdAsync(userId);

            // Her bir gemi için koordinatları kontrol et
            foreach (var ship in ships)
            {
                var coordinates = await _coordinateRepository.GetByShipIdAsync(ship.ShipId);
                if (coordinates.Any(coord => coord.Row == row && coord.Col == col))
                {
                    // İsabet bulundu
                    return true;
                }
            }

            // İsabet bulunamadı
            return false;
        }

        public Task<bool> CheckGameOverAsync(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
