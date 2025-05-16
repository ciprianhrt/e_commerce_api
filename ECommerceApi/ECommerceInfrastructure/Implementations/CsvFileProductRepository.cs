using ECommerceApplication.Repositories;
using ECommerceDomain;
using ECommerceInfrastructure.CsvFileRepo;

namespace ECommerceInfrastructure.Implementations;

public class CsvFileProductRepository : IProductRepository
{
    public async Task<Product?> SearchProductByIdAsync(Guid id)
    {
        return FileReader.ReadById(id);
    }

    public async Task<List<Product>> SearchProductsAsync()
    {
        return FileReader.ReadAll();
    }

    public async Task AddProductAsync(Product product)
    {
        FileWriter.Write(product);
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        FileWriter.RemoveAt(id);
    }

    public Task<List<Product>> SearchProductByName(string? requestProductName)
    {
        throw new NotImplementedException();
    }
}