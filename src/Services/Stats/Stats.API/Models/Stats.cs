using Shared.Base.Models;

namespace Stats.API.Models;

public class Stats : BaseEntity
{
    public List<Like> Likes { get; set; } = new();
    public List<Dislike> Dislikes { get; set; } = new();

}