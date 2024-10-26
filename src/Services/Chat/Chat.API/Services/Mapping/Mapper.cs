using AutoMapper;
using Chat.API.Models;
using Chat.API.Models.Dto;

namespace Chat.API.Services.Mapping;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Message, MessageDto>().ReverseMap();
        CreateMap<Models.Chat, ChatDto>().ReverseMap();
    }
}