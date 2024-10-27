using Shared.Base.Models;

namespace Stats.API.Models.Dto;

public class StatsDto : BaseDto
{
    public List<Like> Likes { get; set; } = new();
    public List<Dislike> Dislikes { get; set; } = new();
}