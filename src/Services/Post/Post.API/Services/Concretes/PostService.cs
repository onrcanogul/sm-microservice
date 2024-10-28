using AutoMapper;
using MassTransit;
using Newtonsoft.Json;
using Post.API.Contexts;
using Post.API.Models;
using Post.API.Models.Dtos;
using Post.API.Services.Abstracts;
using Shared.Base;
using Shared.Base.Repository;
using Shared.Base.Repository.Outbox;
using Shared.Base.Service;
using Shared.Base.UnitOfWork;
using Shared.Events;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Post.API.Services.Concretes;

public class PostService(
    IRepository<Models.Post, PostDbContext> repository,
    IMapper mapper,
    IUnitOfWork<PostDbContext> unitOfWork,
    ISendEndpointProvider sendEndpointProvider,
    PostDbContext context)
    : ApplicationCrudService<Models.Post,PostDto,PostDbContext>(repository, mapper, unitOfWork), IPostService
{
    public async Task<ServiceResponse<List<PostDto>>> GetByUser(Guid userId)
    {
        var posts = await repository.GetListAsync(x => x.UserId == userId);
        var dto = mapper.Map<List<PostDto>>(posts);
        return ServiceResponse<List<PostDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<NoContent>> Create(PostDto dto)
    {
        dto.Id = Guid.NewGuid();
        await repository.CreateAsync(mapper.Map<Models.Post>(dto));
        
        PostCreatedEvent @event = new() { IdempotentToken = new Guid(), PostId = dto.Id.Value, };
        await context.PostOutboxes.AddAsync(new PostOutbox
        {
            IdempotentToken = @event.IdempotentToken,
            OccuredOn = DateTime.UtcNow,
            Payload = JsonSerializer.Serialize(@event),
            ProcessedOn = null,
            Type = nameof(PostCreatedEvent)
        });

        await unitOfWork.CommitAsync();
        
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }
}