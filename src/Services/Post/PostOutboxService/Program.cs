using MassTransit;
using PostOutboxService.Jobs;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(conf =>
{
    conf.UsingRabbitMq((context, configure) =>
    {
        configure.Host(builder.Configuration.GetConnectionString("RabbitMqConnection"));
    });
});
builder.Services.AddQuartz(configure =>
{
    JobKey jobKey = new("PostOutboxPublishJob");
    configure.AddJob<OutboxTablePublisherJob>(opt => opt.WithIdentity(jobKey));

    TriggerKey triggerKey = new("PostOutboxPublishTrigger");
    configure.AddTrigger(options => options.ForJob(jobKey)
        .WithIdentity(triggerKey)
        .StartAt(DateTime.UtcNow)
        .WithSimpleSchedule(builder => builder
            .WithIntervalInSeconds(60)
            .RepeatForever()));
});
builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);


var host = builder.Build();
host.Run();


