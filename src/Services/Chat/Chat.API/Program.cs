using System.Reflection;
using Chat.API.Contexts;
using Chat.API.Services.Abstacts;
using Chat.API.Services.Concretes;
using Chat.API.Services.Hubs;
using Shared.Base.Extensions;
using Mapper = Chat.API.Services.Mapping.Mapper;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddDbContext<ChatDbContext>();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Mapper)));
builder.Services.AddEfCoreServices();
builder.Services.AddSignalR();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();
