using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Events;
using Stats.API.Context;
using Stats.API.Models;
using Stats.API.Models.Dto;
using Stats.API.Models.Inbox;
using Stats.API.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Stats.API.Consumers;

public class CommentCreatedEventConsumer(StatsDbContext dbContext, IStatsService<CommentStats, CommentStatsDto, StatsDbContext> service) : IConsumer<CommentCreatedEvent>
{
    public async Task Consume(ConsumeContext<CommentCreatedEvent> context)
    {
        var result = await dbContext.PostInboxes.AnyAsync(x => x.IdempotentToken == context.Message.IdempotentToken);
        if (!result)
        {
            var postInbox = new PostInbox()
            {
                IdempotentToken = context.Message.IdempotentToken,
                Payload = JsonSerializer.Serialize(context.Message),
                Processed = false
            };
            await dbContext.PostInboxes.AddAsync(postInbox);
            await dbContext.SaveChangesAsync();
        }
        var outboxes = await dbContext.PostInboxes.Where(x => !x.Processed).ToListAsync();

        foreach (var outbox in outboxes)
        {
            var commentCreatedEvent = JsonSerializer.Deserialize<CommentCreatedEvent>(outbox.Payload);
            if(commentCreatedEvent == null) continue;
            
            await service.CreateAsync(new CommentStatsDto { CommentId = commentCreatedEvent.CommentId });
            outbox.Processed = true;
            await dbContext.SaveChangesAsync();
        }
        
    }
}