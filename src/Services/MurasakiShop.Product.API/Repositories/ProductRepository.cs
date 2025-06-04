using Microsoft.EntityFrameworkCore;
using MurasakiShop.Product.API.DTOs;

namespace MurasakiShop.Product.API.Repositories;

public class ProductRepository(ProductContext context) : IProductRepository
{
    private readonly ProductContext _context = context;

    public async Task<Models.Product> CreateAsync(Models.Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Products
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Models.Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Images)
            .ToListAsync();
    }

    public async Task<IEnumerable<Models.Product>> GetByCategoryAsync(Guid categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Images)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<Models.Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Models.Product> UpdateAsync(Models.Product product)
    {
        product.UpdatedAt = DateTime.UtcNow;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
}