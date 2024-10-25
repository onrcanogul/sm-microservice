namespace Notification.API.Models;

public abstract class NotificationType
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public string Title { get; set; } = null!;
}