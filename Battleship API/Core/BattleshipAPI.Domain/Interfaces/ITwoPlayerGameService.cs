using BattleshipAPI.Domain.Entites;

namespace BattleshipAPI.Domain.Interfaces
{
    public interface ITwoPlayerGameService
    {
        // Yeni bir oyun başlatır
        Task<int> StartGameAsync(int player1Id, int player2Id);

        // Oyuncunun hamlesini işler
        Task<bool> MakeMoveAsync(int gameId, int playerId, int chosenNumber);

        // Oyunun durumunu döner
        Task<TwoPlayerGame> GetGameStatusAsync(int gameId);

        // Oyun listesini döner
        Task<IEnumerable<TwoPlayerGame>> GetAllGamesAsync();

        // Oyunu sonuçlandırır
        Task<bool> EndGameAsync(int gameId, string result);

        // Oyuncunun hamlelerini getirir
        Task<IEnumerable<int>> GetPlayerMovesAsync(int gameId, int playerId);

        // Tankların yerleştirildiği pozisyonları getirir
        Task<IEnumerable<int>> GetTankPositionsAsync(int gameId, int playerId);
        Task<bool> CheckHitAsync(int row, int col, int gameId, int userId);
        Task<bool> CheckGameOverAsync(int gameId);
    }
}
