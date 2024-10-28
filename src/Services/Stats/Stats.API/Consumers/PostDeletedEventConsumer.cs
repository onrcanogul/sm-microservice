using System.Text.Json;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Stats.API.Context;
using Stats.API.Models;
using Stats.API.Models.Dto;
using Stats.API.Models.Inbox;
using Stats.API.Services;

namespace Stats.API.Consumers;

public class PostDeletedEventConsumer(StatsDbContext dbContext, IStatsService<PostStats, PostStatsDto, StatsDbContext> service) : IConsumer<PostDeletedEvent>
{
    public async Task Consume(ConsumeContext<PostDeletedEvent> context)
    {
        var result = await dbContext.PostInboxes.AnyAsync(x => x.IdempotentToken == context.Message.IdempotentToken);
        if (!result)
        {
            var newInbox = new PostInbox
            {
                IdempotentToken = context.Message.IdempotentToken,
                Payload = JsonSerializer.Serialize(context.Message),
                Processed = false
            };
            await dbContext.PostInboxes.AddAsync(newInbox);
            await dbContext.SaveChangesAsync();
        }
        var inboxes = await dbContext.PostInboxes.Where(x => !x.Processed).ToListAsync();
        foreach (var inbox in inboxes)
        {
            var @event = JsonSerializer.Deserialize<PostDeletedEvent>(inbox.Payload);
            if(@event == null) continue;
            service.DeleteAsync(@event.PostId);
            inbox.Processed = true;
            await dbContext.SaveChangesAsync(); 
        }
        
        
    }
}