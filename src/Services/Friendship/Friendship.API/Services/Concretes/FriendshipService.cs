using AutoMapper;
using Friendship.API.Contexts;
using Friendship.API.Models;
using Friendship.API.Models.Dto;
using Friendship.API.Services.Abstracts;
using Shared.Base;
using Shared.Base.Repository;
using Shared.Base.Service;
using Shared.Base.UnitOfWork;

namespace Friendship.API.Services.Concretes;

public class FriendshipService(IRepository<Models.Friendship, FriendshipDbContext> repository, IMapper mapper, IUnitOfWork<FriendshipDbContext> unitOfWork) :
    ApplicationCrudService<Models.Friendship, FriendshipDto, FriendshipDbContext>(repository, mapper, unitOfWork), IFriendshipService
{
    public async Task<ServiceResponse<List<FriendshipDto>>> GetFriends(Guid receiverId)
    {
        var friendships = await repository.GetListAsync(x => (x.ReceiverId == receiverId) && x.Status == FriendshipStatus.Accepted);
        var dto = mapper.Map<List<Models.Friendship>, List<FriendshipDto>>(friendships);
        return ServiceResponse<List<FriendshipDto>>.Success(dto, StatusCodes.Status200OK);
    }
    
    public async Task<ServiceResponse<List<FriendshipDto>>> GetRejectedFriends(Guid receiverId)
    {
        var friendships = await repository.GetListAsync(x => (x.ReceiverId == receiverId) && x.Status == FriendshipStatus.Rejected);
        var dto = mapper.Map<List<Models.Friendship>, List<FriendshipDto>>(friendships);
        return ServiceResponse<List<FriendshipDto>>.Success(dto, StatusCodes.Status200OK);
    }
    
    public async Task<ServiceResponse<List<FriendshipDto>>> GetPendings(Guid receiverId)
    {
        var friendships = await repository.GetListAsync(x => (x.ReceiverId == receiverId) && x.Status == FriendshipStatus.Pending);
        var dto = mapper.Map<List<Models.Friendship>, List<FriendshipDto>>(friendships);
        return ServiceResponse<List<FriendshipDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<List<FriendshipDto>>> GetSentRequests(Guid senderId)
    {
        var friendships = await repository.GetListAsync(x => x.SenderId == senderId);
        var dto = mapper.Map<List<Models.Friendship>, List<FriendshipDto>>(friendships);
        return ServiceResponse<List<FriendshipDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<NoContent>> Send(Guid senderId, Guid receiverId)
    {
        if (repository.GetQueryable().Any(x => (x.SenderId == senderId && x.ReceiverId == receiverId) && x.Status != FriendshipStatus.Rejected))
            throw new Exception();
        var friendship = new Models.Friendship
        {
            SenderId = senderId, ReceiverId = receiverId,
            Status = FriendshipStatus.Pending
        };
        await repository.CreateAsync(friendship);
        await unitOfWork.CommitAsync();
        
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }

    public async Task<ServiceResponse<NoContent>> Accept(Guid friendshipId)
    {
        var friendship = await repository.GetFirstOrDefaultAsync(x => x.Id == friendshipId);
        if(friendship == null) throw new Exception();
        friendship.Status = FriendshipStatus.Accepted;
        repository.Update(friendship);
        await unitOfWork.CommitAsync();
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }
    
    public async Task<ServiceResponse<NoContent>> Reject(Guid friendshipId)
    {
        var friendship = await repository.GetFirstOrDefaultAsync(x => x.Id == friendshipId);
        if(friendship == null) throw new Exception();
        friendship.Status = FriendshipStatus.Rejected;
        repository.Update(friendship);
        await unitOfWork.CommitAsync();
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }
    
}