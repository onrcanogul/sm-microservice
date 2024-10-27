using Microsoft.EntityFrameworkCore;

namespace Comment.API.Contexts;

public class CommentDbContext(DbContextOptions<CommentDbContext> options) : DbContext(options)
{
    public DbSet<Models.Comment> Comments { get; set; }
    public DbSet<Models.CommentOutbox> CommentOutboxes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Models.Comment>().HasQueryFilter(i => !i.IsDeleted);
    }
}