using Shared.Events.Base;

namespace Shared.Events;

public class CommentCreatedEvent : IEvent
{
    public Guid IdempotentToken { get; set; }
    public Guid CommentId { get; set; }
}