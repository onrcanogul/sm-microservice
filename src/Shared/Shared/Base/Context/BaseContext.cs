using Microsoft.EntityFrameworkCore;
using Shared.Base.Models;

namespace Shared.Base.Context;

public class BaseDbContext(DbContextOptions<BaseDbContext> options) : DbContext(options)
{
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var dataList = ChangeTracker.Entries<BaseEntity>().ToList();

        foreach (var data in dataList)
        {
            var baseEntity = data.Entity;
            switch (data.State)
            {
                case EntityState.Modified:
                    baseEntity.UpdatedDate = DateTime.UtcNow;
                    break;
                case EntityState.Added:
                    baseEntity.CreatedDate = DateTime.UtcNow;
                    baseEntity.UpdatedDate = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        var dataList = ChangeTracker.Entries<BaseEntity>().ToList();

        foreach (var data in dataList)
        {
            var baseEntity = data.Entity;
            switch (data.State)
            {
                case EntityState.Modified:
                    baseEntity.UpdatedDate = DateTime.UtcNow;
                    break;
                case EntityState.Added:
                    baseEntity.CreatedDate = DateTime.UtcNow;
                    baseEntity.UpdatedDate = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChanges();
    }
}