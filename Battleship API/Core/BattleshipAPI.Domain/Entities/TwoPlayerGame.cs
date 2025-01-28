using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Domain.Entites
{
    public class TwoPlayerGame
    {
        public int GameId { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public String GameResult { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
