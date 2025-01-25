using AutoMapper;
using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Domain.Entites;

namespace Battleship_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
