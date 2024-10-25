using AutoMapper;
using Notification.API.Contexts;
using Notification.API.Models.Dto;
using Notification.API.Services.Abstarcts;
using Shared.Base.Repository;
using Shared.Base.Service;
using Shared.Base.UnitOfWork;

namespace Notification.API.Services.Concretes;

public class NotificationService(IRepository<Models.Notification, NotificationDbContext> repository, IMapper mapper, IUnitOfWork<NotificationDbContext> unitOfWork) :
    ApplicationCrudService<Models.Notification, NotificationDto, NotificationDbContext>(repository, mapper, unitOfWork), INotificationService
{
    
}