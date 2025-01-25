namespace BattleshipAPI.Application.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string role { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int TotalPoints { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
