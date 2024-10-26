using Shared.Base.Models;

namespace Chat.API.Models.Dto;

public class MessageDto : BaseDto
{
    public Guid ChatId { get; set; } 
    public Guid SenderId { get; set; }
    public string Content { get; set; } = null!;
    public ChatDto Chat { get; set; }
}