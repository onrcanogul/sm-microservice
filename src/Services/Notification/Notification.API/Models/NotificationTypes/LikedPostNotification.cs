namespace Notification.API.Models;

public class LikedPostNotification : NotificationType
{
    public Guid PostId { get; set; }
}