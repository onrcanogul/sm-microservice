using AutoMapper;
using Comment.API.Contexts;
using Comment.API.Models.Dto;
using Comment.API.Services.Abstracts;
using Shared.Base;
using Shared.Base.Repository;
using Shared.Base.Service;
using Shared.Base.UnitOfWork;

namespace Comment.API.Services.Concretes;

public class CommentService(IRepository<Models.Comment, CommentDbContext> repository, IMapper mapper, IUnitOfWork<CommentDbContext> unitOfWork) 
    : ApplicationCrudService<Models.Comment, CommentDto, CommentDbContext>(repository, mapper, unitOfWork), ICommentService
{
    public async Task<ServiceResponse<List<CommentDto>>> GetByPost(Guid postId)
    {
        var comments = await repository.GetListAsync(p => p.PostId == postId);
        var dto = mapper.Map<List<CommentDto>>(comments);
        return ServiceResponse<List<CommentDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<List<CommentDto>>> GetByUser(Guid userId)
    {
        var comments = await repository.GetListAsync(p => p.UserId == userId);
        var dto = mapper.Map<List<CommentDto>>(comments);
        return ServiceResponse<List<CommentDto>>.Success(dto, StatusCodes.Status200OK);
    }
}