using ECommerceApplication.Commands;
using ECommerceApplication.Repositories;
using ECommerceDomain;
using MediatR;

namespace ECommerceApplication.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var updatedProduct = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price
        };
        await _productRepository.UpdateProductAsync(updatedProduct);
        
        return updatedProduct;
    }
}