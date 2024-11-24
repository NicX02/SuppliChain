using SuppliChain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SuppliChain.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the User table (optional, for more explicit setup)
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId); // Set Id as Primary Key
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.Role).IsRequired().HasMaxLength(50);
        });
    }
}