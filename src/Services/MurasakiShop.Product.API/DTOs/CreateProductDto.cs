namespace MurasakiShop.Product.API.DTOs;

public record CreateProductDto
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public Guid CategoryId { get; init; }
    public Guid MaterialId { get; init; }
    public string Dimensions { get; init; } = string.Empty;
    public decimal Weight { get; init; }
    public string Color { get; init; } = string.Empty;
    public bool IsHandmade { get; init; }
    public string Glazing { get; init; } = string.Empty;
}