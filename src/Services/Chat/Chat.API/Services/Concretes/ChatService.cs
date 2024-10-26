using AutoMapper;
using Chat.API.Contexts;
using Chat.API.Models;
using Chat.API.Models.Dto;
using Chat.API.Services.Abstacts;
using Chat.API.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using Shared.Base;
using Shared.Base.Repository;
using Shared.Base.Service;
using Shared.Base.UnitOfWork;

namespace Chat.API.Services.Concretes;

public class ChatService(IRepository<Models.Chat, ChatDbContext> repository, IMapper mapper, IUnitOfWork<ChatDbContext> unitOfWork,IHubContext<ChatHub> hubContext,IRepository<Message, ChatDbContext> messageRepository)
    : ApplicationCrudService<Models.Chat, ChatDto, ChatDbContext>(repository, mapper, unitOfWork), IChatService
{
    public async Task<ServiceResponse<NoContent>> SendMessage(string content, Guid user1Id, Guid user2Id)
    {
        var chat = await repository.GetFirstOrDefaultAsync(x => (x.User1Id == user1Id && x.User2Id == user2Id) || (x.User1Id == user2Id && x.User2Id == user1Id));
        await messageRepository.CreateAsync(new Message
        {
            Id = Guid.NewGuid(),
            ChatId = chat.User1Id,
            SenderId = chat.User2Id,
            Content = content,
            IsDeleted = false
        });
        await Task.WhenAll(unitOfWork.CommitAsync(), hubContext.Clients.All.SendAsync("ReceiveMessage"));
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }

    public async Task<ServiceResponse<NoContent>> Create(Guid user1Id, Guid user2Id)
    {
        var chat = await repository.GetFirstOrDefaultAsync(x => (x.User1Id == user1Id && x.User2Id == user2Id) || (x.User1Id == user2Id && x.User2Id == user1Id));
        if(chat != null)
            throw new NullReferenceException();
        await repository.CreateAsync(new() {User1Id = user1Id, User2Id = user2Id}); 
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }
}