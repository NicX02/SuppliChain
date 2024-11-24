using SuppliChain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SuppliChain.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User>? Users { get; set; }
    public DbSet<ItemCategory> ItemCategories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<User>().ToTable("Users");
        // modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        // modelBuilder.Entity<Student>().ToTable("Student");

        // User-Transaction
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId);

        // Item-Category
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CategoryId);

        // Item-Stock
        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Item)
            .WithMany(i => i.Stock)
            .HasForeignKey(s => s.ItemId);

        // Item-Transaction
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Item)
            .WithMany(i => i.Transactions)
            .HasForeignKey(t => t.ItemId);
    }
}
