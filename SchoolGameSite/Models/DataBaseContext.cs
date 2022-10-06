using Microsoft.EntityFrameworkCore;

namespace SchoolGameSite.Models;

public sealed class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public DataBaseContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
}