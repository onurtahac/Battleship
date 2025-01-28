using Microsoft.AspNetCore.Mvc;
using BattleshipAPI.Application.Interfaces;
using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Battleship_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ITwoPlayerGameAppService _twoPlayerGameAppService;
        private readonly ICoordianteAppService _coordinateAppService;
        private const int Player2Id = 6; // Sabit Player2 ID

        public GameController(ITwoPlayerGameAppService twoPlayerGameAppService, ICoordianteAppService coordinateAppService)
        {
            _twoPlayerGameAppService = twoPlayerGameAppService;
            _coordinateAppService = coordinateAppService;
        }

        // Oyun başlatma
        [HttpPost("start")]
        public async Task<IActionResult> StartGame([FromBody] int player1Id)
        {
            // Yeni oyun oluşturuluyor
            var newGame = new TwoPlayerGame
            {
                GameId = new Random().Next(1, 10000),
                Player1Id = player1Id,
                Player2Id = Player2Id,
                CreatedAt = DateTime.Now,
                GameResult = "In Progress",
                Users = new List<Users>() // Kullanıcılar daha sonra eklenecek
            };

            var createdGame = await _twoPlayerGameAppService.StartGameAsync(player1Id, Player2Id);

            if (createdGame == null)
            {
                return BadRequest("Game creation failed.");
            }

            return Ok(createdGame);
        }

        [HttpPost("move")]
        public async Task<IActionResult> MakeMove([FromBody] CoordinatesDTOs move)
        {
            // Oyun durumunu kontrol et
            var game = await _twoPlayerGameAppService.GetGameStatusAsync(move.UserId);
            if (game == null || (game.Player1Id != move.UserId && game.Player2Id != move.UserId))
            {
                return BadRequest("Invalid move: Player is not part of this game.");
            }

            // Hamleyi işleme
            var coordinate = new Coordinates
            {
                GameId = game.GameId, // Oyun ID'si
                UserId = move.UserId, // Oyuncu ID'si
                Row = move.Row, // Satır
                Col = move.Col, // Sütun
                ShipId = move.ShipId // Gemi ID'si
            };

            // Koordinatı ekle ve sonucunu kontrol et
            var addedCoordinate = await _coordinateAppService.AddAsync(coordinate);
            if (addedCoordinate == null)
            {
                return BadRequest("Invalid move: Failed to add coordinate.");
            }

            // Hamlenin isabet edip etmediğini kontrol et
            var isHit = await _twoPlayerGameAppService.CheckHitAsync(move.Row, move.Col, game.GameId, move.UserId);
            if (isHit)
            {
                // Oyunun bitip bitmediğini kontrol et
                var isGameOver = await _twoPlayerGameAppService.CheckGameOverAsync(game.GameId);
                if (isGameOver)
                {
                    return Ok("Game over! You win!");
                }
                return Ok("Hit!");
            }

            return Ok("Miss!");
        }
        // Oyunun durumunu al
        [HttpGet("status/{gameId}")]
        public async Task<IActionResult> GetGameStatus(int gameId)
        {
            var gameStatus = await _twoPlayerGameAppService.GetGameStatusAsync(gameId);

            if (gameStatus == null)
            {
                return NotFound("Game not found.");
            }

            return Ok(gameStatus);
        }

        // Oyunu bitir
        [HttpPost("end")]
        public async Task<IActionResult> EndGame([FromBody] TwoPlayerGame game)
        {
            // Oyun sonucunu kontrol et
            if (game.GameResult != "Win" && game.GameResult != "Lose" && game.GameResult != "Draw")
            {
                return BadRequest("Invalid game result.");
            }

            // Oyunu bitir
            var result = await _twoPlayerGameAppService.EndGameAsync(game.GameId, game.GameResult);
            if (!result)
            {
                return BadRequest("Error ending the game.");
            }

            return Ok("Game ended successfully.");
        }

        // Gemileri yerleştirme
        [HttpPost("place-ships")]
        public async Task<IActionResult> PlaceShips([FromBody] List<ShipsDTO> ships)
        {
            if (ships == null || ships.Count == 0)
            {
                return BadRequest("No ships to place.");
            }

            foreach (var ship in ships)
            {
                // Gemi koordinatları belirleniyor
                var coordinates = new List<Coordinates>();

                for (int i = 0; i < ship.Length; i++)
                {
                    // Dikey veya yatay yerleşime göre koordinat hesaplama
                    var coordinate = new Coordinates
                    {
                        GameId = ship.UserId, // UserId'yi GameId olarak kullanıyoruz (geçici çözüm)
                        UserId = ship.UserId,
                        ShipId = ship.ShipId,
                        Row = ship.IsVertical ? i : 0, // Örnek hesaplama
                        Col = ship.IsVertical ? 0 : i  // Örnek hesaplama
                    };

                    // Koordinat sınırlarını kontrol et
                    if (coordinate.Row < 0 || coordinate.Row >= 10 || coordinate.Col < 0 || coordinate.Col >= 10)
                    {
                        return BadRequest($"Ship {ship.ShipId} is out of bounds.");
                    }

                    coordinates.Add(coordinate);
                }

                // Koordinatları veri tabanına kaydet
                foreach (var coordinate in coordinates)
                {
                    var addedCoordinate = await _coordinateAppService.AddAsync(coordinate);
                    if (addedCoordinate == null)
                    {
                        return BadRequest($"Failed to place ship {ship.ShipId}.");
                    }
                }
            }

            return Ok("Ships placed successfully.");
        }
    }
}