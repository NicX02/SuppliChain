namespace SuppliChain.Models;

public class Item
{
    public int ItemId { get; set; } // Primary Key
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation Property for category
    public int ItemCategoryId { get; set; }

    public ItemCategory? Category { get; set; }

    // Navigation Property for stock levels of this item
    public Stock Stock { get; set; }


    // Navigation Property for transactions involving this item
    public ICollection<Transaction>? Transactions { get; set; }
}
