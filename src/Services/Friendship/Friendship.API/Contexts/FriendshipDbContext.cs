using Microsoft.EntityFrameworkCore;

namespace Friendship.API.Contexts;

public class FriendshipDbContext(DbContextOptions<FriendshipDbContext> options) : DbContext(options)
{
    public DbSet<Models.Friendship> Friendships { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.Friendship>().HasQueryFilter(i => !i.IsDeleted);
    }
}