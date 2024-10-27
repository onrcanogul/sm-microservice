using Microsoft.EntityFrameworkCore;
using Shared.Base.Extensions;
using Stats.API.Context;
using Stats.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IStatsService<,,>), typeof(StatsService<,,>));
builder.Services.AddEfCoreServices();
builder.Services.AddDbContext<StatsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();