namespace Shared.Base.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync();
    void Commit();
    Task RollbackAsync();
    void Rollback();
}