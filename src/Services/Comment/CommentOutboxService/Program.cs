
using CommentOutboxService.Database;
using CommentOutboxService.Jobs;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<ICommentOutboxDatabase, CommentOutboxDatabase>();
builder.Services.AddQuartz(configure =>
{
    JobKey jobKey = new("CommentOutboxPublishJob");
    configure.AddJob<OutboxTablePublisherJob>(opt => opt.WithIdentity(jobKey));

    TriggerKey triggerKey = new("CommentOutboxPublishTrigger");
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