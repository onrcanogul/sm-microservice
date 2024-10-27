using System.Text.Json;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Base.UnitOfWork;
using Shared.Events;
using Stats.API.Context;
using Stats.API.Models;
using Stats.API.Models.Dto;
using Stats.API.Models.Inbox;
using Stats.API.Services;

namespace Stats.API.Consumers;

public class PostCreatedEventConsumer(StatsDbContext dbContext, IStatsService<PostStats, PostStatsDto, StatsDbContext> service, IUnitOfWork<StatsDbContext> uow) : IConsumer<PostCreatedEvent>
{
    public async Task Consume(ConsumeContext<PostCreatedEvent> context)
    {
        var result = await dbContext.PostInboxes.AnyAsync(x => x.IdempotentToken == context.Message.IdempotentToken);
        if (!result)
        {
            PostInbox postInbox = new()
            { 
                IdempotentToken = context.Message.IdempotentToken,
                Payload = JsonSerializer.Serialize(context.Message), 
                Processed = false
            };
            await dbContext.PostInboxes.AddAsync(postInbox);
            await uow.CommitAsync();
        }
        var inboxes = await dbContext.PostInboxes.Where(x => !x.Processed).ToListAsync();
        foreach (var inbox in inboxes)
        {
            var postInbox = JsonSerializer.Deserialize<PostCreatedEvent>(inbox.Payload);
            if(postInbox == null) continue;
            service.CreateAsync(new PostStatsDto { PostId = postInbox.PostId });
            inbox.Processed = true;
            await uow.CommitAsync();
        }
    }
}