using Post.API.Contexts;
using Post.API.Models.Dtos;
using Shared.Base;
using Shared.Base.Service;

namespace Post.API.Services.Abstracts;

public interface IPostService : IApplicationCrudService<Models.Post, PostDto, PostDbContext>
{
    Task <ServiceResponse<List<PostDto>>> GetByUser(Guid userId);
    Task<ServiceResponse<NoContent>> Create(PostDto dto);
}