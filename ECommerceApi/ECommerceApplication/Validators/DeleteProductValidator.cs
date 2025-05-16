using ECommerceApplication.Commands;
using ECommerceApplication.Repositories;

namespace ECommerceApplication.Validators;

using FluentValidation;
using System.Threading.Tasks;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Id)
            .MustAsync(async (id, _) => await ProductExistsAsync(id))
            .WithMessage("Product with the given ID does not exist.");
    }

    private async Task<bool> ProductExistsAsync(Guid productId)
    {
        var product = await _productRepository.SearchProductByIdAsync(productId);
        return product != null;
    }
}
