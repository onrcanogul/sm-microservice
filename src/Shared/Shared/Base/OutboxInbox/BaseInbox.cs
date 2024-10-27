using System.ComponentModel.DataAnnotations;

namespace Shared.Base.OutboxInbox;

public class BaseInbox
{
    [Key]
    public Guid IdempotentToken { get; set; }
    public bool Processed { get; set; }
    public string Payload { get; set; } = null!;
}