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

        // // Seed Item Categories
        // if (!context.ItemCategories.Any())
        // {
        //     context.ItemCategories.AddRange(
        //         new ItemCategory { Name = "Electronics" },
        //         new ItemCategory { Name = "Furniture" }
        //     );
        //     context.SaveChanges();
        // }

        // // Seed Items and Stock
        // if (!context.Items.Any())
        // {
        //     var electronicsCategory = context.ItemCategories.First(c => c.Name == "Electronics");
        //     var furnitureCategory = context.ItemCategories.First(c => c.Name == "Furniture");

        //     var item1 = new Item
        //     {
        //         Name = "Laptop",
        //         Description = "A high-end laptop",
        //         Price = 1500.00m,
        //         ItemCategoryId = electronicsCategory.Id,
        //         Stock = new Stock { Quantity = 10 }
        //     };

        //     var item2 = new Item
        //     {
        //         Name = "Chair",
        //         Description = "A comfortable chair",
        //         Price = 200.00m,
        //         ItemCategoryId = furnitureCategory.Id,
        //         Stock = new Stock { Quantity = 50 }
        //     };

        //     context.Items.AddRange(item1, item2);
        //     context.SaveChanges();
        // }
    }
}

// Seed Users
// if (context.Users?.Any() != true)
// {
// var users = new[]
// {
//         new User { Name = "Admin", Password = "admin", Role = "admin", CreatedAt = DateTime.Now },
//         new User { Name = "Normal", Password = "normal", Role = "employee", CreatedAt = DateTime.Now },
//     };

// context.Users?.AddRange(users);
// context.SaveChanges();
// // }


//     // Seed Item Categories
//     if (!context.ItemCategories.Any())
//     {
//         var categories = new ItemCategory[]
//         {
//             new() { Name = "Electronics", Description = "Electronic items and gadgets" },
//             new() { Name = "Office Supplies", Description = "Stationery and office tools" },
//             new() { Name = "Furniture", Description = "Office and home furniture" },
//         };

//         context.ItemCategories.AddRange(categories);
//         context.SaveChanges();
//     }

//     // Seed Items
//     if (!context.Items.Any())
//     {
//         var items = new[]
//         {
//             new Item { Name = "Laptop", Description = "Dell Inspiron 15", CategoryId = 1, CreatedAt = DateTime.Now },
//             new Item { Name = "Office Chair", Description = "Ergonomic chair", CategoryId = 3, CreatedAt = DateTime.Now },
//             new Item { Name = "Printer Paper", Description = "A4 size paper (500 sheets)", CategoryId = 2, CreatedAt = DateTime.Now },
//         };

//         context.Items.AddRange(items);
//         context.SaveChanges();
//     }

//     // Seed Stock
//     if (!context.Stocks.Any())
//     {
//         var stock = new[]
//         {
//             new Stock { ItemId = 1, Quantity = 10, Location = "Warehouse A", LastUpdated = DateTime.Now },
//             new Stock { ItemId = 2, Quantity = 20, Location = "Warehouse B", LastUpdated = DateTime.Now },
//             new Stock { ItemId = 3, Quantity = 50, Location = "Warehouse A", LastUpdated = DateTime.Now },
//         };

//         context.Stocks.AddRange(stock);
//         context.SaveChanges();
//     }

//     // Seed Transactions
//     if (!context.Transactions.Any())
//     {
//         var transactions = new[]
//         {
//             new Transaction
//             {
//                 ItemId = 1, UserId = 1, Quantity = 2, Type = "add", Timestamp = DateTime.Now, Remarks = "Initial stock entry"
//             },
//             new Transaction
//             {
//                 ItemId = 2, UserId = 1, Quantity = 5, Type = "add", Timestamp = DateTime.Now, Remarks = "Initial stock entry"
//             },
//             new Transaction
//             {
//                 ItemId = 3, UserId = 2, Quantity = 10, Type = "remove", Timestamp = DateTime.Now, Remarks = "Office use"
//             },
//         };

//         context.Transactions.AddRange(transactions);
//         context.SaveChanges();
//     }
// }
// }
