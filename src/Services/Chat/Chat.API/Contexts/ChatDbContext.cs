using Microsoft.EntityFrameworkCore;

namespace Chat.API.Contexts;

public class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
{
    public DbSet<Models.Chat> Chats { get; set; }
    public DbSet<Models.Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.Chat>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<Models.Message>().HasQueryFilter(c => !c.IsDeleted);
    }
}