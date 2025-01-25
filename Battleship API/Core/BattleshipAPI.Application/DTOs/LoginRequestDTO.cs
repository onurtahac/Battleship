using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Application.DTOs
{
    public class LoginRequestDTO
    {
        public string EmailAdress { get; set; }
        public string Password { get; set; }
    }
}
