using Microsoft.EntityFrameworkCore;

namespace Stats.API.Context;

public class StatsDbContext(DbContextOptions<StatsDbContext> options) : DbContext(options)
{
    public DbSet<Models.Stats> Stats { get; set; }
    public DbSet<Models.Inbox.PostInbox> PostInboxes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.Stats>().HasQueryFilter(x => !x.IsDeleted);
    }
}