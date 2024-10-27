using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Base;
using Shared.Base.Repository;
using Shared.Base.Service;
using Shared.Base.UnitOfWork;
using Stats.API.Context;
using Stats.API.Models.Dto;

namespace Stats.API.Services;

public class StatsService<T, TDto, TContext>(IRepository<T, TContext> repository, IMapper mapper, IUnitOfWork<TContext> unitOfWork) :ApplicationCrudService<T,TDto,TContext>(repository, mapper, unitOfWork),IStatsService<T, TDto, TContext>
where T : Models.Stats where TDto : StatsDto where TContext : StatsDbContext
{
}