using AutoMapper;
using Post.API.Models.Dtos;

namespace Post.API.Services.Mapping;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<PostDto, Models.Post>().ReverseMap();
    }
}