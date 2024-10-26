using Shared.Base.Models;

namespace Post.API.Models.Dtos;

public class PostDto : BaseDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid UserId { get; set; }
}