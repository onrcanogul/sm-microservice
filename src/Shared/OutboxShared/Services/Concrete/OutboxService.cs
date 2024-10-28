using System.Text.Json;
using System.Transactions;
using OutboxShared.Base;
using OutboxShared.Database;
using OutboxShared.Services.Abstraction;

namespace OutboxShared.Services.Concrete;

public class OutboxService<T>(IOutboxDatabase database) : IOutboxService<T> where T : BaseOutbox
{
    public async Task<List<T>> GetNotProcessedOutboxes()
        => (await database.QueryAsync<T>(
            "SELECT * FROM CommentOutboxes WHERE ProcessedOn IS NULL ORDER BY OccuredOn ASC")).ToList();

    public async Task Process(Func<T, Task> processOutboxAction)
    {
        if (database.DataReaderState)
        {
            database.DataReaderBusy();
            var outboxes = await GetNotProcessedOutboxes();
            foreach (var outbox in outboxes)
            {
                using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                await processOutboxAction(outbox);
                await UpdateProcessedOn(outbox);
                transactionScope.Complete();
            }
            database.DataReaderReady();
        }
        
        
    }
    private async Task UpdateProcessedOn(T outbox)
    {
        await database.ExecuteAsync($"UPDATE CommentOutboxes SET PROCESSEDON = GETDATE() WHERE IdempotentToken = '{outbox.IdempotentToken}'");
    }
    
    
}