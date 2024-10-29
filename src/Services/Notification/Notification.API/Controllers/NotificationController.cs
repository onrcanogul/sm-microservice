using Microsoft.AspNetCore.Mvc;
using Notification.API.Models.Dto;
using Notification.API.Services.Abstarcts;
using Shared.Base.Controller;

namespace Notification.API.Controllers;

public class NotificationController(INotificationService service) : AbstractBaseController
{
    [HttpGet("secure/sender/{senderId:guid}")]
    public async Task<IActionResult> GetBySender([FromRoute] Guid senderId)
        => ControllerResponse(await service.GetListAsync(x => x.SenderId == senderId));
    
    [HttpGet("secure/receiver/{receiverId:guid}")]
    public async Task<IActionResult> GetByReceiver([FromRoute] Guid receiverId)
        => ControllerResponse(await service.GetListAsync(x => x.ReceiverId == receiverId));

    [HttpPost("secure")]
    public async Task<IActionResult> Create(NotificationDto dto)
        => ControllerResponse(await service.CreateAsync(dto));
    
    [HttpPut("secure")]
    public async Task<IActionResult> Update(NotificationDto dto)
        => ControllerResponse(await service.UpdateAsync(dto));
    
    [HttpDelete("secure/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
        => ControllerResponse(await service.DeleteAsync(id));
    
}