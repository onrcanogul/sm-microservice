using Shared.Base.Models;

namespace Comment.API.Models.Dto;

public class CommentDto : BaseDto
{
    public string Text { get; set; } = null!;
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}