using Shared.Base.Models;

namespace Friendship.API.Models;

public class Friendship : BaseEntity
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public FriendshipStatus Status { get; set; }
}