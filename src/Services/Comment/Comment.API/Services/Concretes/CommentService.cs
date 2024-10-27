using System.Text.Json;
using AutoMapper;
using Comment.API.Contexts;
using Comment.API.Models;
using Comment.API.Models.Dto;
using Comment.API.Services.Abstracts;
using Shared.Base;
using Shared.Base.Repository;
using Shared.Base.Repository.Outbox;
using Shared.Base.Service;
using Shared.Base.UnitOfWork;
using Shared.Events;

namespace Comment.API.Services.Concretes;

public class CommentService(IRepository<Models.Comment, CommentDbContext> repository, IMapper mapper, IUnitOfWork<CommentDbContext> unitOfWork, IOutboxRepository<CommentOutbox, CommentDbContext> outboxRepository) 
    : ApplicationCrudService<Models.Comment, CommentDto, CommentDbContext>(repository, mapper, unitOfWork), ICommentService
{
    public async Task<ServiceResponse<List<CommentDto>>> GetByPost(Guid postId)
    {
        var comments = await repository.GetListAsync(p => p.PostId == postId);
        var dto = mapper.Map<List<CommentDto>>(comments);
        return ServiceResponse<List<CommentDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<List<CommentDto>>> GetByUser(Guid userId)
    {
        var comments = await repository.GetListAsync(p => p.UserId == userId);
        var dto = mapper.Map<List<CommentDto>>(comments);
        return ServiceResponse<List<CommentDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<NoContent>> Create(CommentDto dto)
    {
        dto.Id = Guid.NewGuid();
        await repository.CreateAsync(mapper.Map<Models.Comment>(dto));

        var commentCreatedEvent = new CommentCreatedEvent
        {
            CommentId = dto.Id.Value, IdempotentToken = Guid.NewGuid()
        };
        CommentOutbox outbox = new()
        {
            IdempotentToken = Guid.NewGuid(),
            OccuredOn = DateTime.UtcNow,
            Payload = JsonSerializer.Serialize(commentCreatedEvent),
            ProcessedOn = null,
            Type = nameof(CommentCreatedEvent)
        };

        await outboxRepository.SaveEventAsync(outbox);
        await unitOfWork.CommitAsync();
        
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }
}