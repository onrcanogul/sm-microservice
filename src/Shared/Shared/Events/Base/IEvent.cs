namespace Shared.Events.Base;

public interface IEvent
{
    public Guid IdempotentToken { get; set; }
}