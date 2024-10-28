using Shared.Events.Base;

namespace Shared.Events;

public class PostDeletedEvent : IEvent
{
    public Guid IdempotentToken { get; set; }
    public Guid PostId { get; set; }
}