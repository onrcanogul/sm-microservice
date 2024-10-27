using Shared.Events.Base;

namespace Shared.Events;

public class PostCreatedEvent : IEvent
{
    public Guid PostId { get; set; }
    public Guid IdempotentToken { get; set; }
}