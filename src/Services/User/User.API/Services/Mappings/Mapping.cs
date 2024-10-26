using AutoMapper;
using User.API.Models.Dtos;

namespace User.API.Services.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Models.User, UserDto>().ReverseMap();
        CreateMap<Models.User, RegisterDto>().ReverseMap();
        CreateMap<Models.User, LoginDto>().ReverseMap();
    }
}