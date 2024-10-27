using System.Text.Json;
using MassTransit;
using PostOutboxService.Database;
using PostOutboxService.Models;
using Quartz;
using Shared;
using Shared.Events;

namespace PostOutboxService.Jobs;

public class OutboxTablePublisherJob(IPostOutboxDatabase database, ISendEndpointProvider sendEndpointProvider) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        if (database.DataReaderState)
        {
            database.DataReaderBusy();
            var outboxes = await database.QueryAsync<PostOutbox>("SELECT * FROM PostOutbox WHERE ProcessedOn IS NULL ORDER BY OccuredOn ASC");
            foreach (var outbox in outboxes)
            {
                if (outbox.Type != nameof(PostCreatedEvent)) continue;
                
                var postCreatedEvent = JsonSerializer.Deserialize<PostCreatedEvent>(outbox.Payload);
                if (postCreatedEvent == null) continue;
                
                var sendEndPoint = await sendEndpointProvider.GetSendEndpoint(new Uri(QueueSettings.Post_Stats_Post_Created_Event_Queue));
                await sendEndPoint.Send(postCreatedEvent);
            }
            
        }
    }
}