namespace SuppliChain.Models;

public class User
{
    public int UserId { get; set; } // Primary Key
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; } // e.g., "admin", "staff"
    public DateTime CreatedAt { get; set; }

    // Navigation Property for transactions made by the user
    public ICollection<Transaction>? Transactions { get; set; }
}
