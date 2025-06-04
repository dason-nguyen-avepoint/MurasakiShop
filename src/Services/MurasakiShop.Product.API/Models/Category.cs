
using System.ComponentModel.DataAnnotations;

namespace MurasakiShop.Product.API.Models;

public class Category
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty; // "Bowls", "Vases", "Plates", "Cups"

    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}