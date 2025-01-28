using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Domain.Entities
{
    public class Ships
    {
        [Key]
        public int ShipId { get; set; }
        public int UserId { get; set; }
        public int Length { get; set; }
        public bool IsVertical { get; set; }
    }
}
