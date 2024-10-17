using Shared.Base.Context;

namespace Shared.Base.UnitOfWork;

public class UnitOfWork(BaseDbContext context) : IUnitOfWork
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