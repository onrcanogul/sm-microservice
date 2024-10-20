using Post.API.Contexts;
using Post.API.Models.Dtos;
using Shared.Base.Service;

namespace Post.API.Services.Abstracts;

public interface IPostService : IApplicationCrudService<Models.Post, PostDto, PostDbContext>
{
    
}