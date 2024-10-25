using Shared.Base.Models;

namespace Post.API.Models;

public class Post : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid StatusId { get; set; }
    public Guid UserId { get; set; }
}