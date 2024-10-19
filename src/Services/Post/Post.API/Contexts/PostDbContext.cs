using Microsoft.EntityFrameworkCore;
using Shared.Base.Context;

namespace Post.API.Contexts;

public class PostDbContext(DbContextOptions<PostDbContext> options) : DbContext(options)
{
    
}