namespace BattleshipAPI.Application.DTOs
{
    public class TwoPlayerGame
    {
        public int GameId { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public String GameResult { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Users> Users { get; set; }

    }

    public class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TotalPoints { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
