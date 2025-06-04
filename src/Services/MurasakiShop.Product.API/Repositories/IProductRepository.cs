namespace MurasakiShop.Product.API.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Models.Product>> GetAllAsync();
    Task<Models.Product?> GetByIdAsync(Guid id);
    Task<Models.Product> CreateAsync(Models.Product product);
    Task<Models.Product> UpdateAsync(Models.Product product);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Models.Product>> GetByCategoryAsync(Guid categoryId);
    Task<bool> ExistsAsync(Guid id);
}