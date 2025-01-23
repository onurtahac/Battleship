
namespace BattleshipAPI.Application.DTOs
{
    public class SoloGame
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int TankLocations { get; set; }
        public int ChosenNumbers { get; set; }
        public int GameResult { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Users> Userss { get; set; }

    }

    public class Userss
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
