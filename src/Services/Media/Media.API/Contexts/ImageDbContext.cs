using Media.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Media.API.Contexts;

public class ImageDbContext(DbContextOptions<ImageDbContext> options) : DbContext(options)
{
    public DbSet<Image> Images { get; set; }
}