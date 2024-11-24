
namespace SuppliChain.Models;

public class Stock
{
    public int StockId { get; set; } // Primary Key
    public int ItemId { get; set; } // Foreign Key referencing Item
    public int Quantity { get; set; }
    public string? Location { get; set; } // Optional: e.g., "Warehouse A"
    public DateTime LastUpdated { get; set; }

    // Navigation Property for item
    public Item? Item { get; set; }
}
