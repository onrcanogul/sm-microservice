using Friendship.API.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Base.Controller;

namespace Friendship.API.Controllers;

public class FriendshipController(IFriendshipService service) : AbstractBaseController
{
    [HttpGet("get-friends/{receiverId:guid}")]
    public async Task<IActionResult> GetFriends([FromRoute] Guid receiverId)
        => ControllerResponse(await service.GetFriends(receiverId));
    
    [HttpGet("secure/get-rejected-friends/{receiverId:guid}")]
    public async Task<IActionResult> GetRejectedFriends([FromRoute] Guid receiverId)
        => ControllerResponse(await service.GetRejectedFriends(receiverId));
    
    [HttpGet("secure/get-pending/{receiverId:guid}")]
    public async Task<IActionResult> GetPendings([FromRoute] Guid receiverId)
        => ControllerResponse(await service.GetPendings(receiverId));
    
    [HttpGet("secure/get-sent-request/{receiverId:guid}")]
    public async Task<IActionResult> GetSentRequest([FromRoute] Guid receiverId)
        => ControllerResponse(await service.GetSentRequests(receiverId));
    
    [HttpPost("secure/send/{senderId:guid}/{receiverId:guid}")]
    public async Task<IActionResult> Send([FromRoute] Guid senderId, [FromRoute] Guid receiverId)
        => ControllerResponse(await service.Send(senderId, receiverId));
    
    [HttpPost("secure/accept/{friendshipId:guid}")]
    public async Task<IActionResult> Accept([FromRoute] Guid friendshipId)
        => ControllerResponse(await service.Accept(friendshipId));
    
    [HttpPost("secure/reject/{friendshipId:guid}")]
    public async Task<IActionResult> Reject([FromRoute] Guid friendshipId)
        => ControllerResponse(await service.Reject(friendshipId));

    [HttpDelete("secure/{friendshipId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid friendshipId)
        => ControllerResponse(await service.DeleteAsync(friendshipId));
}