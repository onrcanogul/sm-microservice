using Microsoft.EntityFrameworkCore;

namespace Friendship.API.Contexts;

public class FriendshipDbContext : DbContext
{
    public DbSet<Models.Friendship> Friendships { get; set; }
}