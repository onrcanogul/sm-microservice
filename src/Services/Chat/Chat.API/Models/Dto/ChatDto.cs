using System.ComponentModel.DataAnnotations.Schema;
using Shared.Base.Models;

namespace Chat.API.Models.Dto;

public class ChatDto : BaseDto
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Content { get; set; } = null!;
    [NotMapped] public override DateTime UpdatedDate { get; set; }
}