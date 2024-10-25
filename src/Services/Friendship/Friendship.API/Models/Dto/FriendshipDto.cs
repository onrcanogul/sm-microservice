using Shared.Base.Models;

namespace Friendship.API.Models.Dto;

public class FriendshipDto : BaseDto
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public FriendshipStatus Status { get; set; }
}