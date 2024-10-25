using Friendship.API.Contexts;
using Friendship.API.Models.Dto;
using Shared.Base;
using Shared.Base.Service;

namespace Friendship.API.Services.Abstracts;

public interface IFriendshipService : IApplicationCrudService<Models.Friendship, FriendshipDto, FriendshipDbContext>
{
    Task<ServiceResponse<List<FriendshipDto>>> GetFriends(Guid receiverId);
    Task<ServiceResponse<List<FriendshipDto>>> GetRejectedFriends(Guid receiverId);
    Task<ServiceResponse<List<FriendshipDto>>> GetPendings(Guid receiverId);
    Task<ServiceResponse<List<FriendshipDto>>> GetSentRequests(Guid senderId);
    Task<ServiceResponse<NoContent>> Send(Guid senderId, Guid receiverId);
    Task<ServiceResponse<NoContent>> Accept(Guid friendshipId);
    Task<ServiceResponse<NoContent>> Reject(Guid friendshipId);
}