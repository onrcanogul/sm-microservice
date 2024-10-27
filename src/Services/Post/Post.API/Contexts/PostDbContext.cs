using Microsoft.EntityFrameworkCore;
using Shared.Base.Context;

namespace Post.API.Contexts;

public class PostDbContext(DbContextOptions<PostDbContext> options) : DbContext(options)
{
    public DbSet<Models.Post> Posts { get; set; }
    public DbSet<Models.PostOutbox> PostOutboxes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Models.Post>().HasQueryFilter(i => !i.IsDeleted);
    }
}