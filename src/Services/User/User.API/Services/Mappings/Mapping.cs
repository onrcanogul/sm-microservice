using AutoMapper;
using User.API.Models.Dtos;

namespace User.API.Services.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Models.User, UserDto>();
    }
}