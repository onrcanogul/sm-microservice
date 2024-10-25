using Microsoft.EntityFrameworkCore;

namespace Comment.API.Contexts;

public class CommentDbContext : DbContext
{
    public DbSet<Models.Comment> Comments { get; set; }
}