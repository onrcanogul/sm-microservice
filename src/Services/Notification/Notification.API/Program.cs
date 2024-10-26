using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Notification.API.Contexts;
using Notification.API.Services.Abstarcts;
using Notification.API.Services.Concretes;
using Notification.API.Services.Mapping;
using Shared.Base.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEfCoreServices();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddDbContext<NotificationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Mapper)));
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

