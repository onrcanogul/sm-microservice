using Microsoft.Extensions.DependencyInjection;
using Shared.Base.Repository;
using Shared.Base.Repository.Outbox;
using Shared.Base.Service;
using Shared.Base.UnitOfWork;

namespace Shared.Base.Extensions;

public static class SharedExtension
{
    public static IServiceCollection AddEfCoreServices(this IServiceCollection services)
    { 
        services.AddScoped(typeof(IApplicationCrudService<,,>), typeof(ApplicationCrudService<,,>));
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        return services;
    }

    public static IServiceCollection AddInboxOutboxServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IOutboxRepository<,>), typeof(OutboxRepository<,>));
        return services;
    }
}