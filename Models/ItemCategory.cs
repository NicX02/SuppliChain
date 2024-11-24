using System.ComponentModel.DataAnnotations;

namespace SuppliChain.Models;

public class ItemCategory
{
    [Key]
    public int CategoryId { get; set; } // Primary Key
    public string? Name { get; set; }
    public string? Description { get; set; }

    // Navigation Property for items in this category
    public ICollection<Item>? Items { get; set; }
}
