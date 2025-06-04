using MurasakiShop.Product.API.DTOs;

namespace MurasakiShop.Product.API.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(Guid id);
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
    Task DeleteProductAsync(Guid id);
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(Guid categoryId);
}