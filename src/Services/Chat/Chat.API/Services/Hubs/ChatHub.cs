using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Services.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(Guid chatId, Guid senderId, string content)
    {
        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", senderId, content);
    }
}