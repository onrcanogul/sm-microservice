using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Base.Context;
using Shared.Base.Models;
using Shared.Base.Repository;
using Shared.Base.UnitOfWork;

namespace Shared.Base.Service;

public class ApplicationCrudService<T, TDto, TContext>(IRepository<T, TContext> repository, IMapper mapper, IUnitOfWork<TContext> unitOfWork) 
    : IApplicationCrudService<T, TDto, TContext> where T : BaseEntity where TDto : BaseDto where TContext : DbContext
{
    public async Task<ServiceResponse<List<TDto>>> GetListAsync(Expression<Func<T?, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? includeProperties = null,
        bool disableTracking = true)
    {
        var list = await repository.GetListAsync(predicate, orderBy, includeProperties, disableTracking);
        var dto = mapper.Map<List<TDto>>(list);
        return ServiceResponse<List<TDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<TDto>> GetFirstOrDefaultAsync(Expression<Func<T?, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? includeProperties = null,
        bool disableTracking = true)
    {
        var entity = await repository.GetFirstOrDefaultAsync(predicate, orderBy, includeProperties, disableTracking);
        var dto = mapper.Map<TDto>(entity);
        return ServiceResponse<TDto>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<List<TDto>>> GetPagedListAsync(int page, int size, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IQueryable<T>>? includeProperties = null, bool disableTracking = true)
    {
        var list = await repository.GetPagedListAsync(page, size, predicate, orderBy, includeProperties, disableTracking);
        var dto = mapper.Map<List<TDto>>(list);
        return ServiceResponse<List<TDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<TDto>> CreateAsync(TDto dto)
    {
        await repository.CreateAsync(mapper.Map<T>(dto));
        await unitOfWork.CommitAsync();
        return ServiceResponse<TDto>.Success(dto, StatusCodes.Status201Created);
    }

    public async Task<ServiceResponse<TDto>> UpdateAsync(TDto dto)
    {
        var entity = await repository.GetFirstOrDefaultAsync(x => x.Id == dto.Id);
        repository.Update(entity);
        await unitOfWork.CommitAsync();
        return ServiceResponse<TDto>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<NoContent>> DeleteAsync(Guid id)
    {
        var entity = await repository.GetFirstOrDefaultAsync(x => x.Id == id);
        repository.Delete(entity);
        await unitOfWork.CommitAsync();
        return ServiceResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
    
    private IQueryable<T> GetCommon(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IQueryable<T>> include = null, bool disableTracking = true)
    {
        IQueryable<T?> query = repository.GetQueryable();
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