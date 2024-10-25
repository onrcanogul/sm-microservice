using Comment.API.Models.Dto;
using Comment.API.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Comment.API;

public static class Endpoints
{
    public static WebApplication AddEndpoints(this WebApplication app)
    {
        app.MapGet("/comment/post/{postId:guid}", ([FromRoute] Guid postId, ICommentService service) => 
        service.GetByPost(postId));

        app.MapGet("/comment/user/{userId:guid}", ([FromRoute] Guid userId,ICommentService service) => 
        service.GetByUser(userId));

        app.MapPost("/comment", (ICommentService service, CommentDto dto) => 
            service.CreateAsync(dto));

        app.MapPut("/comment", (ICommentService service, CommentDto dto) => 
            service.UpdateAsync(dto));
    
        app.MapDelete("/comment/{id:guid}", (ICommentService service, Guid id) => 
            service.DeleteAsync(id));

        return app;
    }
}