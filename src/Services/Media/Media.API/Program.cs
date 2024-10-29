using Media.API.Contexts;
using Media.API.Models;
using Media.API.Services.Abstract;
using Media.API.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Base.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEfCoreServices();
builder.Services.AddScoped(typeof(IMediaService<>), typeof(MediaService<>));
builder.Services.AddDbContext<ImageDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/secure/post/media/{productId:guid}",async (IMediaService<PostImage> mediaService,[FromForm] IFormFileCollection files,[FromRoute] Guid productId) 
    => await mediaService.Upload(files,"post-images",productId));
    
app.MapPost("/api/secure/user/media/{userId:guid}", async (IMediaService<UserImage> mediaService, [FromForm] IFormFileCollection files,[FromRoute] Guid userId) 
    => await mediaService.Upload(files,"user-images",userId));

app.Run();

