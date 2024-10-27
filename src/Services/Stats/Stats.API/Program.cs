using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Base.Extensions;
using Shared.Events;
using Stats.API.Consumers;
using Stats.API.Context;
using Stats.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IStatsService<,,>), typeof(StatsService<,,>));
builder.Services.AddEfCoreServices();
builder.Services.AddDbContext<StatsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddMassTransit(conf =>
{
    conf.AddConsumer<PostCreatedEventConsumer>();
    conf.AddConsumer<CommentCreatedEventConsumer>();
    conf.UsingRabbitMq((context, configure) =>
    {
        configure.Host(builder.Configuration.GetConnectionString("RabbitMqConnection"));
        configure.ReceiveEndpoint(QueueSettings.Post_Stats_Post_Created_Event_Queue, e => e.ConfigureConsumer<PostCreatedEventConsumer>(context));
        configure.ReceiveEndpoint(QueueSettings.Comment_Stats_Comment_Created_Event_Queue, e => e.ConfigureConsumer<CommentCreatedEventConsumer>(context));
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();