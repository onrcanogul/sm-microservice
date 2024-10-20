using System.Reflection;
using Post.API.Contexts;
using Post.API.Services.Abstracts;
using Post.API.Services.Concretes;
using Post.API.Services.Mapping;
using Shared.Base.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEfCoreServices();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Mapping)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PostDbContext>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
