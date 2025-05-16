using ECommerceApplication.Commands;
using ECommerceApplication.Repositories;
using ECommerceDomain;
using MediatR;

namespace ECommerceApplication.Handlers;

public class SearchProductHandler : IRequestHandler<SearchProductCommand, List<Product>>
{
    private readonly IProductRepository _productRepository;

    public SearchProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> Handle(SearchProductCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.ProductName))
        {
            return await _productRepository.SearchProductByName(request.ProductName);
        }
        
        if (!request.Id.HasValue)
        {
            return await _productRepository.SearchProductsAsync();
        }
       
        var product = await _productRepository.SearchProductByIdAsync(request.Id.Value);
        return product == null ? [] : [product];
    }
}