using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Base.Context;
using Shared.Base.Models;

namespace Shared.Base.Repository;

public class Repository<T, TContext>(TContext context)
    : IRepository<T, TContext> where T : BaseEntity where TContext : DbContext
{
    private DbSet<T> Table => context.Set<T>();

    public IQueryable<T> GetQueryable()
    {
        return Table.AsQueryable();
    }

    public async Task<List<T>> GetListAsync(Expression<Func<T?, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? include = null,
        bool disableTracking = true)
    {
        var query = GetCommon(predicate, orderBy, include, disableTracking);
        return await query.ToListAsync();
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T?, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? include = null,
        bool disableTracking = true)
    {
        var query = GetCommon(predicate, orderBy, include, disableTracking);
        return await query.FirstOrDefaultAsync();
    }

    public Task<List<T>> GetPagedListAsync(int page, int size, Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null, bool disableTracking = true)
    {
        var query = GetCommon(predicate, orderBy, include, disableTracking);
        return query.Skip((page - 1) * size).Take(size).ToListAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
        Update(entity);
    }

    private IQueryable<T> GetCommon(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null, bool disableTracking = true)
    {
        IQueryable<T> query = GetQueryable();
        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (orderBy != null)
        {
            query = orderBy(query!);
        }

        return query;
    }
}