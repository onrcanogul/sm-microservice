using OutboxShared.Base;

namespace OutboxShared.Services.Abstraction;

public interface IOutboxService<T> where T : BaseOutbox
{
    Task<List<T>> GetNotProcessedOutboxes();
    Task Process(Func<T, Task> processOutboxAction);
}