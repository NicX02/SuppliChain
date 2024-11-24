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
    public DbSet<ItemCategory> ItemCategories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);

    //     // Configure the User table (optional, for more explicit setup)
    // modelBuilder.Entity<User>(entity =>
    // {
    //     entity.HasKey(u => u.UserId); // Set Id as Primary Key
    //     entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
    //     entity.Property(u => u.Password).IsRequired();
    //     entity.Property(u => u.Role).IsRequired().HasMaxLength(50);
    // });

    // }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define relationships
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.ItemCategoryId);

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Item)
            .WithOne(i => i.Stock)
            .HasForeignKey<Stock>(s => s.ItemId);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Item)
            .WithMany()
            .HasForeignKey(t => t.ItemId);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);
    }
}