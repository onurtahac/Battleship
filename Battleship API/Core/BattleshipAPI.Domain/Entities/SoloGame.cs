using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Domain.Entites
{
    public class SoloGame
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int TankLocations { get; set; }
        public int ChosenNumbers { get; set; }
        public int GameResult { get; set;}
        public DateTime CreatedAt { get; set; }
    }
}
