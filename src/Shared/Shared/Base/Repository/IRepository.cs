using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Base.Context;
using Shared.Base.Models;

namespace Shared.Base.Repository;

public interface IRepository<T,TContext> where T : BaseEntity where TContext : DbContext
{
    IQueryable<T> GetQueryable();
    Task<List<T>> GetListAsync(Expression<Func<T?, bool>>? predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IQueryable<T>>? includeProperties = null,
        bool disableTracking = true);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T?, bool>>? predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IQueryable<T>>? includeProperties = null,
        bool disableTracking = true);
    Task<List<T>> GetPagedListAsync(int page, int size,Expression<Func<T, bool>>? predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IQueryable<T>>? includeProperties = null,
        bool disableTracking = true);
    
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}