using Shared.Base.Models;

namespace Chat.API.Models;

public class Message : BaseEntity
{
    public Guid ChatId { get; set; } 
    public Guid SenderId { get; set; }
    public string Content { get; set; } = null!;
    public Chat Chat { get; set; }
}