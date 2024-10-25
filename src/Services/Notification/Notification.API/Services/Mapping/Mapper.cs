using AutoMapper;
using Notification.API.Models.Dto;

namespace Notification.API.Services.Mapping;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Models.Notification, NotificationDto>().ReverseMap();
    }   
}