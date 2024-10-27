using System.ComponentModel.DataAnnotations;

namespace Shared.Base.OutboxInbox;

public class BaseOutbox
{
    [Key]
    public Guid IdempotentToken { get; set; }
    public DateTime OccuredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string Type { get; set; } = null!;
    public string Payload { get; set; } = null!;
}