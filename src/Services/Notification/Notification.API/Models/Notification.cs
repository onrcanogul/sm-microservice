using Shared.Base.Models;

namespace Notification.API.Models;

public class Notification : BaseEntity
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public int NotificationTypeId { get; set; }
    public NotificationType NotificationType { get; set; }
}