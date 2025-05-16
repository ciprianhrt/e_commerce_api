using ECommerceApplication.Commands;
using ECommerceApplication.Repositories;
using ECommerceDomain;
using MediatR;

namespace ECommerceApplication.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price
        };
        await _productRepository.AddProductAsync(product);
        
        return product;
    }
}