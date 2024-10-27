using Microsoft.EntityFrameworkCore;
using Shared.Base.OutboxInbox;

namespace Shared.Base.Repository.Outbox;

public interface IOutboxRepository<TOutbox,TContext> where TContext : DbContext where TOutbox : BaseOutbox
{
    Task SaveEventAsync(TOutbox outbox);
}