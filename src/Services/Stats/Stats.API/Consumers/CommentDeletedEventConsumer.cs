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

public class CommentDeletedEventConsumer(StatsDbContext dbContext, IStatsService<CommentStats, CommentStatsDto, StatsDbContext> service) : IConsumer<CommentDeletedEvent>
{
    public async Task Consume(ConsumeContext<CommentDeletedEvent> context)
    {
        var result = await dbContext.CommentInboxes.AnyAsync(x => x.IdempotentToken == context.Message.IdempotentToken);
        if (!result)
        {
            var commentInbox = new CommentInbox()
            {
                IdempotentToken = context.Message.IdempotentToken,
                Payload = JsonSerializer.Serialize(context.Message),
                Processed = false
            };
            await dbContext.CommentInboxes.AddAsync(commentInbox);
            await dbContext.SaveChangesAsync();
        }
        var inboxes = await dbContext.CommentInboxes.Where(x => !x.Processed).ToListAsync();
        foreach (var inbox in inboxes)
        {
            var commentDeletedEvent = JsonSerializer.Deserialize<CommentDeletedEvent>(inbox.Payload);
            if(commentDeletedEvent == null) continue;
            service.DeleteAsync(commentDeletedEvent.CommentId);
            inbox.Processed = true;
            await dbContext.SaveChangesAsync();
        }
    }
}