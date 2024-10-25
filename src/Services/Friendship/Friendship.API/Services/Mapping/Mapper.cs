using AutoMapper;
using Friendship.API.Models.Dto;

namespace Friendship.API.Services.Mapping;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Models.Friendship, FriendshipDto>().ReverseMap();
    }
}