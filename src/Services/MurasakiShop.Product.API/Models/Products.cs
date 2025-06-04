using System.ComponentModel.DataAnnotations;

namespace MurasakiShop.Product.API.Models;

public class Product
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public Guid MaterialId { get; set; }
    public Material? Material { get; set; }

    public string Dimensions { get; set; } = string.Empty; // "20x15x10 cm"

    [Range(0, double.MaxValue)]
    public decimal Weight { get; set; } // in kg

    public string Color { get; set; } = string.Empty;
    public bool IsHandmade { get; set; }
    public string Glazing { get; set; } = string.Empty; // "Matte", "Glossy", "Textured"

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<ProductImage> Images { get; set; } = [];
}