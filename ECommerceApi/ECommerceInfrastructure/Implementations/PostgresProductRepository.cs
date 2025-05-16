using ECommerceApplication.Repositories;
using ECommerceDomain;
using Microsoft.EntityFrameworkCore;

namespace ECommerceInfrastructure.Implementations;

public class PostgresProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public PostgresProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> SearchProductByIdAsync(Guid id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public async Task<List<Product>> SearchProductsAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task AddProductAsync(Product product)
    { 
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    { 
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid id)
    { 
        _dbContext.Products.Remove(await _dbContext.Products.FindAsync(id));
    }

    public async Task<List<Product>> SearchProductByName(string? requestProductName)
    {
        return await _dbContext.Products.Where(p => 
            p.Name.Equals(requestProductName, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
    }
}