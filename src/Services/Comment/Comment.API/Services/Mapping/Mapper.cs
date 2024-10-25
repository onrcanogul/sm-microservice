using AutoMapper;
using Comment.API.Models.Dto;

namespace Comment.API.Services.Mapping;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Models.Comment, CommentDto>().ReverseMap();
    }
}