using SuppliChain.Data;
using SuppliChain.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SuppliChain.Data;

public class DbInitializer
{
    public static void Initialize(DatabaseContext context)
    {
        // Recreate the database
        context.Database.EnsureCreated();
        context.Database.ExecuteSqlRaw("DELETE FROM Users WHERE Role = 'admin';");// todo remove
        // Seed initial data

        //seed user
        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Name = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                Role = "admin",
                CreatedAt = DateTime.UtcNow
            });
            context.SaveChanges();
        }

        // Seed Item Categories
        if (!context.ItemCategories.Any())
        {
            context.ItemCategories.AddRange(
                new ItemCategory { Name = "Electronics", Description = "Electronics" },
                new ItemCategory { Name = "Furniture", Description = "Furniture" }
            );
            context.SaveChanges();
        }

        // Seed Items and Stock
        if (!context.Items.Any())
        {
            var electronicsCategory = context.ItemCategories.First(c => c.Name == "Electronics");
            var furnitureCategory = context.ItemCategories.First(c => c.Name == "Furniture");

            var item1 = new Item
            {
                Name = "Laptop",
                Description = "A high-end laptop",
                Price = 1749.99m,
                ItemCategoryId = electronicsCategory.CategoryId,
                Stock = new Stock { Quantity = 10 }
            };

            var item2 = new Item
            {
                Name = "Chair",
                Description = "A comfortable chair",
                Price = 200.00m,
                ItemCategoryId = furnitureCategory.CategoryId,
                Stock = new Stock { Quantity = 50 }
            };
            context.Items.AddRange(item1, item2);
            context.SaveChanges();
        }

        // Seed Transactions
        if (!context.Transactions.Any())
        {
            var firstUser = context.Users.OrderBy(u => u.UserId).First();
            var firstItem = context.Items.OrderBy(i => i.ItemId).First();
            var secondItem = context.Items.OrderBy(i => i.ItemId).Skip(1).First();
            var transactions = new[]
            {
                new Transaction
                {
                    UserId = firstUser.UserId,
                    ItemId = firstItem.ItemId,
                    Quantity = 10,
                    Type = "add",
                    Timestamp = DateTime.Now.AddDays(-2),
                    Remarks = "Aquired 10 units"
                },
                new Transaction
                {
                    UserId = firstUser.UserId,
                    ItemId = secondItem.ItemId,
                    Quantity = 70,
                    Type = "add",
                    Timestamp = DateTime.Now.AddDays(-1),
                    Remarks = "Aquired 70 units"
                },
                new Transaction
                {
                    UserId = firstUser.UserId,
                    ItemId = secondItem.ItemId,
                    Quantity = 10,
                    Type = "remove",
                    Timestamp = DateTime.Now,
                    Remarks = "Sold 10 units"
                },
            };
            context.Transactions.AddRange(transactions);
            context.SaveChanges();
        }

    }
}