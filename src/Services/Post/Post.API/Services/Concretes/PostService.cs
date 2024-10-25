using AutoMapper;
using Post.API.Contexts;
using Post.API.Models.Dtos;
using Post.API.Services.Abstracts;
using Shared.Base;
using Shared.Base.Repository;
using Shared.Base.Service;
using Shared.Base.UnitOfWork;

namespace Post.API.Services.Concretes;

public class PostService(IRepository<Models.Post, PostDbContext> repository, IMapper mapper, IUnitOfWork<PostDbContext> unitOfWork) : ApplicationCrudService<Models.Post,PostDto,PostDbContext>(repository, mapper, unitOfWork), IPostService
{
    public async Task<ServiceResponse<List<PostDto>>> GetByUser(Guid userId)
    {
        var posts = await repository.GetListAsync(x => x.UserId == userId);
        var dto = mapper.Map<List<PostDto>>(posts);
        return ServiceResponse<List<PostDto>>.Success(dto, StatusCodes.Status200OK);
    }
    
}