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
        // Ensure the database is created
        context.Database.EnsureCreated();

        // Seed Users
        // if (context.Users?.Any() != true)
        // {
        var users = new[]
        {
                new User { Name = "Admin", Password = "admin", Role = "admin", CreatedAt = DateTime.Now },
                new User { Name = "Normal", Password = "normal", Role = "employee", CreatedAt = DateTime.Now },
            };

        context.Users?.AddRange(users);
        context.SaveChanges();
        // }


        // Seed Item Categories
        if (!context.ItemCategories.Any())
        {
            var categories = new ItemCategory[]
            {
                new() { Name = "Electronics", Description = "Electronic items and gadgets" },
                new() { Name = "Office Supplies", Description = "Stationery and office tools" },
                new() { Name = "Furniture", Description = "Office and home furniture" },
            };

            context.ItemCategories.AddRange(categories);
            context.SaveChanges();
        }

        // Seed Items
        if (!context.Items.Any())
        {
            var items = new[]
            {
                new Item { Name = "Laptop", Description = "Dell Inspiron 15", CategoryId = 1, CreatedAt = DateTime.Now },
                new Item { Name = "Office Chair", Description = "Ergonomic chair", CategoryId = 3, CreatedAt = DateTime.Now },
                new Item { Name = "Printer Paper", Description = "A4 size paper (500 sheets)", CategoryId = 2, CreatedAt = DateTime.Now },
            };

            context.Items.AddRange(items);
            context.SaveChanges();
        }

        // Seed Stock
        if (!context.Stocks.Any())
        {
            var stock = new[]
            {
                new Stock { ItemId = 1, Quantity = 10, Location = "Warehouse A", LastUpdated = DateTime.Now },
                new Stock { ItemId = 2, Quantity = 20, Location = "Warehouse B", LastUpdated = DateTime.Now },
                new Stock { ItemId = 3, Quantity = 50, Location = "Warehouse A", LastUpdated = DateTime.Now },
            };

            context.Stocks.AddRange(stock);
            context.SaveChanges();
        }

        // Seed Transactions
        if (!context.Transactions.Any())
        {
            var transactions = new[]
            {
                new Transaction
                {
                    ItemId = 1, UserId = 1, Quantity = 2, Type = "add", Timestamp = DateTime.Now, Remarks = "Initial stock entry"
                },
                new Transaction
                {
                    ItemId = 2, UserId = 1, Quantity = 5, Type = "add", Timestamp = DateTime.Now, Remarks = "Initial stock entry"
                },
                new Transaction
                {
                    ItemId = 3, UserId = 2, Quantity = 10, Type = "remove", Timestamp = DateTime.Now, Remarks = "Office use"
                },
            };

            context.Transactions.AddRange(transactions);
            context.SaveChanges();
        }
    }
}