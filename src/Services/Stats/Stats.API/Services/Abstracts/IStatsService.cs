using Shared.Base.Service;
using Stats.API.Context;
using Stats.API.Models.Dto;

namespace Stats.API.Services;

public interface IStatsService<T,TDto,TContext> : IApplicationCrudService<T,TDto,TContext> 
where T : Models.Stats where TDto : StatsDto where TContext : StatsDbContext
{
    
}