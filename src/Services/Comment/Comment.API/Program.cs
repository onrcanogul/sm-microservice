using System.Reflection;
using Comment.API;
using Comment.API.Contexts;
using Comment.API.Services.Abstracts;
using Comment.API.Services.Concretes;
using Comment.API.Services.Mapping;
using Shared.Base.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEfCoreServices();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddDbContext<CommentDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Mapper)));

var app = builder.Build();
app.AddEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

