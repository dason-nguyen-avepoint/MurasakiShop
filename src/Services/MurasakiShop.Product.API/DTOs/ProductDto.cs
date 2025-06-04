namespace MurasakiShop.Product.API.DTOs;

public record ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public string MaterialName { get; init; } = string.Empty;
    public string Dimensions { get; init; } = string.Empty;
    public decimal Weight { get; init; }
    public string Color { get; init; } = string.Empty;
    public bool IsHandmade { get; init; }
    public string Glazing { get; init; } = string.Empty;
    public List<ProductImageDto> Images { get; init; } = new();
}