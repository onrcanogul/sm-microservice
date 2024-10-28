using System.Text.Json;
using System.Transactions;
using MassTransit;
using OutboxShared.Database;
using OutboxShared.Services.Abstraction;
using PostOutboxService.Database;
using PostOutboxService.Models;
using Quartz;
using Shared;
using Shared.Events;

namespace PostOutboxService.Jobs;

public class OutboxTablePublisherJob(IOutboxDatabase database, IOutboxService<PostOutbox> service, ISendEndpointProvider sendEndpointProvider) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await service.Process(async outbox =>
        {
            switch (outbox.Type)
            {
                case nameof(PostCreatedEvent):
                {
                    var postCreatedEvent = JsonSerializer.Deserialize<PostCreatedEvent>(outbox.Payload);
                    if (postCreatedEvent == null) throw new NullReferenceException();
                    var sendEndPoint = await sendEndpointProvider.GetSendEndpoint(
                            new Uri(QueueSettings.Post_Stats_Post_Created_Event_Queue));
                    await sendEndPoint.Send(postCreatedEvent);
                    break;
                }
                case nameof(PostDeletedEvent):
                {
                    var postDeletedEvent = JsonSerializer.Deserialize<PostDeletedEvent>(outbox.Payload);
                    if (postDeletedEvent == null) throw new NullReferenceException();
                    var sendEndPoint = await sendEndpointProvider.GetSendEndpoint(
                        new Uri(QueueSettings.Post_Stats_Post_Deleted_Event_Queue));
                    await sendEndPoint.Send(postDeletedEvent);
                    break;
                }
                default:
                    throw new NullReferenceException();
            }
        });
    }
}