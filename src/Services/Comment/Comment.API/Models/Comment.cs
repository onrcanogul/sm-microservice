using Shared.Base.Models;

namespace Comment.API.Models;

public class Comment : BaseEntity
{
    public string Text { get; set; } = null!;
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }

}