using ECommerceApplication.Commands;
using ECommerceApplication.Repositories;
using MediatR;

namespace ECommerceApplication.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteProductAsync(request.Id);
        return request.Id;
    }
}