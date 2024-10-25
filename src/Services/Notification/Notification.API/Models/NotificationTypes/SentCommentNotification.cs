namespace Notification.API.Models;

public class SentCommentNotification : NotificationType
{
    public Guid PostId { get; set; }
    public string Comment { get; set; } = null!;
    public Guid CommentId { get; set; }
}