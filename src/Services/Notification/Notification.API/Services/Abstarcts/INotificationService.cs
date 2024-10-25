using Notification.API.Contexts;
using Notification.API.Models.Dto;
using Shared.Base.Service;

namespace Notification.API.Services.Abstarcts;

public interface INotificationService : IApplicationCrudService<Models.Notification, NotificationDto, NotificationDbContext>
{
}