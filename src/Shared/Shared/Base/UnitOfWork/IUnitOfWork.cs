using Microsoft.EntityFrameworkCore;

namespace Shared.Base.UnitOfWork;

public interface IUnitOfWork<TContext> where TContext : DbContext
{
    Task CommitAsync();
    void Commit();
    Task RollbackAsync();
    void Rollback();
}