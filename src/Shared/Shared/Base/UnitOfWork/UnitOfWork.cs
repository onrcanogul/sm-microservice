using Microsoft.EntityFrameworkCore;

namespace Shared.Base.UnitOfWork;

public class UnitOfWork<TContext>(TContext context) : IUnitOfWork<TContext> where TContext : DbContext
{
    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }

    public void Commit()
    {
        context.SaveChanges();
    }

    public async Task RollbackAsync()
    {
        throw new NotImplementedException();
    }

    public void Rollback()
    {
        throw new NotImplementedException();
    }
}