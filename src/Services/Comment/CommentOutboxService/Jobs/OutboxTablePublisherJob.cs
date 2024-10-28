using System.Text.Json;
using System.Transactions;
using CommentOutboxService.Database;
using CommentOutboxService.Models;
using MassTransit;
using OutboxShared.Database;
using OutboxShared.Services.Abstraction;
using Quartz;
using Shared;
using Shared.Events;

namespace CommentOutboxService.Jobs;

public class OutboxTablePublisherJob(IOutboxDatabase database, IOutboxService<CommentOutbox> service, ISendEndpointProvider sendEndpointProvider) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    { 
        await service.Process(async outbox =>
        {
            if (outbox.Type == nameof(CommentCreatedEvent))
            {
                var postCreatedEvent = JsonSerializer.Deserialize<PostCreatedEvent>(outbox.Payload);
                if (postCreatedEvent == null) throw new NullReferenceException();

                var sendEndPoint =
                    await sendEndpointProvider.GetSendEndpoint(
                        new Uri(QueueSettings.Comment_Stats_Comment_Created_Event_Queue));
                await sendEndPoint.Send(postCreatedEvent);
            }
        });
    }
}