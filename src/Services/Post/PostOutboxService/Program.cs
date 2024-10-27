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
    JobKey jobKey = new("OrderOutboxPublishJob");
    configure.AddJob<OutboxTablePublisherJob>(opt => opt.WithIdentity(jobKey));

    TriggerKey triggerKey = new("OrderOutboxPublishTrigger");
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


