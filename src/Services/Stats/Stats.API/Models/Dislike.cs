using Shared.Base.Models;

namespace Stats.API.Models;

public class Dislike : BaseEntity
{
    public Guid UserId { get; set; }
}