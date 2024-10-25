using System.Reflection;
using Friendship.API.Contexts;
using Friendship.API.Services.Abstracts;
using Friendship.API.Services.Concretes;
using Friendship.API.Services.Mapping;
using Shared.Base.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IFriendshipService, FriendshipService>();
builder.Services.AddEfCoreServices();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Mapper)));
builder.Services.AddDbContext<FriendshipDbContext>();
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
