using AutoMapper;
using MurasakiShop.Product.API.DTOs;
using MurasakiShop.Product.API.Repositories;

namespace MurasakiShop.Product.API.Services;

public class ProductService(IProductRepository repo, IMapper _mapper) : IProductService
{
    private readonly IProductRepository _repo = repo;
    private readonly IMapper _mapper = _mapper;

    public Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<Models.Product>(createProductDto);
        return _repo.CreateAsync(product)
            .ContinueWith(task => _mapper.Map<ProductDto>(task.Result));
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var isProductExists = await _repo.ExistsAsync(id);
        if (!isProductExists)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }
        await _repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(Guid categoryId)
    {
        var products = await _repo.GetByCategoryAsync(categoryId);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
    {
        var isProductExists = await _repo.ExistsAsync(id);
        if (!isProductExists)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }
        _mapper.Map(updateProductDto, await _repo.GetByIdAsync(id));
        var updatedProduct = await _repo.UpdateAsync(_mapper.Map<Models.Product>(updateProductDto));
        return _mapper.Map<ProductDto>(updatedProduct);
    }
}