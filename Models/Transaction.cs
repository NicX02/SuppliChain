namespace SuppliChain.Models;

public class Transaction
{
    public int TransactionId { get; set; } // Primary Key
    public int ItemId { get; set; } // Foreign Key referencing Item
    public int UserId { get; set; } // Foreign Key referencing User
    public int Quantity { get; set; }
    public string? Type { get; set; } // e.g., "add", "remove", "transfer"
    public DateTime Timestamp { get; set; }
    public string? Remarks { get; set; } // Optional notes

    // Navigation Property for user
    public User? User { get; set; }

    // Navigation Property for item
    public Item? Item { get; set; }
}
