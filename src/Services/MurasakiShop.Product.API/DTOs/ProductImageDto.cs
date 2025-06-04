namespace MurasakiShop.Product.API.DTOs;

public record ProductImageDto
{
    public Guid Id { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public string Alt { get; init; } = string.Empty;
    public bool IsMain { get; init; }
}