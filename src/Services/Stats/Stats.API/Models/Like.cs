using Shared.Base.Models;

namespace Stats.API.Models;

public class Like : BaseEntity
{
    public Guid UserId { get; set; }
}