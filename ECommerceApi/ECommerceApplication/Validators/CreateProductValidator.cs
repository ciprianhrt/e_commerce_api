using ECommerceApplication.Commands;
using ECommerceApplication.Repositories;

namespace ECommerceApplication.Validators;
using FluentValidation;
using System.Threading.Tasks;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public CreateProductValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MustAsync(async (name, _) => 
                !await ProductNameExistsAsync(name))
            .WithMessage("A product with this name already exists.");
        
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }

    private async Task<bool> ProductNameExistsAsync(string name)
    {
        return (await _productRepository.SearchProductByName(name)).Count > 0;
    }
}
