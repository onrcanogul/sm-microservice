using CommentOutboxService.Jobs;
using OutboxShared;
using Quartz;
var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
builder.Services.AddOutboxServices<OutboxTablePublisherJob>(builder.Configuration, 60, "CommentOutboxPublishJob", "CommentOutboxPublishTrigger");

var host = builder.Build();
host.Run();