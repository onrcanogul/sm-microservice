using Microsoft.EntityFrameworkCore;
using Notification.API.Models;

namespace Notification.API.Contexts;

public class NotificationDbContext(DbContextOptions<NotificationDbContext> options) : DbContext(options)
{
    public DbSet<Models.Notification> Notifications { get; set; }
    public DbSet<NotificationType> NotificationTypes { get; set; }
    public DbSet<SentCommentNotification> SentCommentNotifications { get; set; }
    public DbSet<FriendshipRequestNotification> FriendshipRequestNotifications { get; set; }
    public DbSet<LikedPostNotification> LikedPostNotifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationType>()
            .HasDiscriminator<string>("NotificationType")
            .HasValue<SentCommentNotification>("SentComment")
            .HasValue<FriendshipRequestNotification>("FriendshipRequest")
            .HasValue<LikedPostNotification>("LikedPost");

        modelBuilder.Entity<Models.Notification>().HasQueryFilter(x => !x.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}