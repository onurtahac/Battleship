using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Domain.Entites
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string role { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int TotalPoints { get; set; }
        public DateTime CreatedAt { get; set; }

        
    }
}
