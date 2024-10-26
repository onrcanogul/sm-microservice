using Chat.API.Contexts;
using Chat.API.Models.Dto;
using Shared.Base;
using Shared.Base.Service;

namespace Chat.API.Services.Abstacts;

public interface IChatService : IApplicationCrudService<Models.Chat, ChatDto, ChatDbContext>
{
    Task<ServiceResponse<NoContent>> SendMessage(string content, Guid user1Id, Guid user2Id);
    Task<ServiceResponse<NoContent>> Create(Guid user1Id, Guid user2Id);
}