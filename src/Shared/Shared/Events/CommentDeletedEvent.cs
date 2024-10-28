using Shared.Events.Base;

namespace Shared.Events;

public class CommentDeletedEvent : IEvent
{
    public Guid IdempotentToken { get; set; }
    public Guid CommentId { get; set; }
}