using Shared.Base.Models;

namespace Notification.API.Models.Dto;

public class NotificationDto : BaseDto
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public int NotificationTypeId { get; set; }
    public NotificationType NotificationType { get; set; }
}