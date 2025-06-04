using System.ComponentModel.DataAnnotations;
namespace MurasakiShop.Product.API.Models;

public class Material
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty; // "Porcelain", "Stoneware", "Earthenware"

    public string Description { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = [];
}