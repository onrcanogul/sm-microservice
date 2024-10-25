using Comment.API.Contexts;
using Comment.API.Models.Dto;
using Shared.Base;
using Shared.Base.Service;

namespace Comment.API.Services.Abstracts;

public interface ICommentService : IApplicationCrudService<Models.Comment, CommentDto, CommentDbContext
>
{
    Task<ServiceResponse<List<CommentDto>>> GetByPost(Guid postId);
    Task<ServiceResponse<List<CommentDto>>> GetByUser(Guid userId);
}