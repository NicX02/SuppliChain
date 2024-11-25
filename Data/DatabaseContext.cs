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

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.TransactionId); // Primary Key
            entity.Property(t => t.Timestamp).IsRequired();
            entity.Property(t => t.Quantity).IsRequired();
            entity.Property(t => t.Type).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Remarks).HasMaxLength(255);

            // Foreign Key for ItemId
            entity.HasOne<Item>()
                  .WithMany() // No navigation property in Item
                  .HasForeignKey(t => t.ItemId)
                  .OnDelete(DeleteBehavior.Restrict); // Adjust delete behavior as needed

            // Foreign Key for UserId
            entity.HasOne<User>()
                  .WithMany() // No navigation property in User
                  .HasForeignKey(t => t.UserId)
                  .OnDelete(DeleteBehavior.Restrict); // Adjust delete behavior as needed
        });
    }
}