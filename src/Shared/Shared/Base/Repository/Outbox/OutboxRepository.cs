using Microsoft.EntityFrameworkCore;
using Shared.Base.OutboxInbox;

namespace Shared.Base.Repository.Outbox;

public class OutboxRepository<TOutbox,TContext>(TContext context) : IOutboxRepository<TOutbox,TContext> where TContext : DbContext where TOutbox : BaseOutbox
{
    public async Task SaveEventAsync(TOutbox outbox)
    {
        await context.Set<TOutbox>().AddAsync(outbox);
    }
}