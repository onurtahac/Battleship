using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Domain.Entities
{
    public class Coordinates
    {
        [Key]
        public int CoordinateId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int ShipId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
