using System.Text.Json;
using System.Transactions;
using CommentOutboxService.Database;
using CommentOutboxService.Models;
using MassTransit;
using Quartz;
using Shared;
using Shared.Events;

namespace CommentOutboxService.Jobs;

public class OutboxTablePublisherJob(ICommentOutboxDatabase database, ISendEndpointProvider sendEndpointProvider) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        if (database.DataReaderState)
        {
            database.DataReaderBusy();
            var outboxes = await database.QueryAsync<CommentOutbox>(
                    "SELECT * FROM CommentOutboxes WHERE ProcessedOn IS NULL ORDER BY OccuredOn ASC");

            foreach (var outbox in outboxes)
            {
                if (outbox.Type == nameof(CommentCreatedEvent))
                {
                    using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    
                    var commentCreatedEvent = JsonSerializer.Deserialize<CommentCreatedEvent>(outbox.Payload);
                    if(commentCreatedEvent == null) return;
                        
                    var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(QueueSettings.Comment_Stats_Comment_Created_Event_Queue));
                    await sendEndpoint.Send(commentCreatedEvent);
                    await UpdateProcessedOn(outbox);
                        
                    transactionScope.Complete();
                }
                //other events
            }
            database.DataReaderReady();
        }
    }
    private async Task UpdateProcessedOn(CommentOutbox outbox)
    {
        await database.ExecuteAsync($"UPDATE CommentOutboxes SET PROCESSEDON = GETDATE() WHERE IdempotentToken = '{outbox.IdempotentToken}'");
    }
}