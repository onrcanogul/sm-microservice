using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OutboxShared.Database;
using OutboxShared.Services.Abstraction;
using OutboxShared.Services.Concrete;
using Quartz;

namespace OutboxShared;

public static class ServiceRegistration
{
    public static IServiceCollection AddOutboxServices<T>(this IServiceCollection services, IConfiguration configuration, int time, string jobKey = "OutboxPublishJob", string triggerKey = "OutboxPublishTrigger")
    where T : IJob
    {
        services.AddMassTransit(conf =>
        {
            conf.UsingRabbitMq((context, configure) =>
            {
                configure.Host(configuration.GetConnectionString("RabbitMqConnection"));
            });
        });
        services.AddQuartz(configure =>
        {
            JobKey jobKey = new("PostOutboxPublishJob");
            configure.AddJob<T>(opt => opt.WithIdentity(jobKey));

            TriggerKey triggerKey = new("PostOutboxPublishTrigger");
            configure.AddTrigger(options => options.ForJob(jobKey)
                .WithIdentity(triggerKey)
                .StartAt(DateTime.UtcNow)
                .WithSimpleSchedule(builder => builder
                    .WithIntervalInSeconds(time)
                    .RepeatForever()));
        });
        services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
        services.AddSingleton<IOutboxDatabase, OutboxDatabase>();
        services.AddSingleton(typeof(IOutboxService<>), typeof(OutboxService<>));
        
        return services;
    }
}