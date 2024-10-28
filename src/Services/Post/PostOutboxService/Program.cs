using OutboxShared;
using PostOutboxService.Jobs;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
builder.Services.AddOutboxServices<OutboxTablePublisherJob>(builder.Configuration, 60, "PostOutboxPublishJob", "PostOutboxPublishTrigger");

var host = builder.Build();
host.Run();


