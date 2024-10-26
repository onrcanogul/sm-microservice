using Chat.API.Services.Abstacts;
using Microsoft.AspNetCore.Mvc;
using Shared.Base.Controller;

namespace Chat.API.Controllers;

public class ChatController(IChatService service) : AbstractBaseController
{
    [HttpGet]
    public async Task<IActionResult> Get(Guid user1Id, Guid user2Id)
        => ControllerResponse(await service.GetFirstOrDefaultAsync(x => (x.User1Id == user1Id && x.User2Id == user2Id) || (x.User1Id == user2Id && x.User2Id == user1Id)));
    [HttpPost]
    public async Task<IActionResult> Create(Guid user1Id, Guid user2Id)
        => ControllerResponse(await service.CreateAsync(new() {ReceiverId = user1Id, SenderId = user2Id}));

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(Guid user1Id, Guid user2Id)
        => ControllerResponse(await service.Create(user1Id, user2Id));
}