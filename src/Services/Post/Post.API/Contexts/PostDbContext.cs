using Microsoft.EntityFrameworkCore;
using Shared.Base.Context;

namespace Post.API.Contexts;

public class PostDbContext(DbContextOptions<PostDbContext> options) : DbContext(options)
{
    public DbSet<Models.Post> Posts { get; set; }
}