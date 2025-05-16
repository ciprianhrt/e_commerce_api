using ECommerceDomain;

namespace ECommerceApplication.Repositories;

public interface IProductRepository
{
    public Task<Product?> SearchProductByIdAsync(Guid id);
    public Task<List<Product>> SearchProductsAsync();
    public Task AddProductAsync(Product product);
    public Task UpdateProductAsync(Product product);
    public Task DeleteProductAsync(Guid id);
    Task<List<Product>> SearchProductByName(string? requestProductName);
}