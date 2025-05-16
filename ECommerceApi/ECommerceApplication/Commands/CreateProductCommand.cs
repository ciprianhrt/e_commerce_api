using ECommerceDomain;
using MediatR;

namespace ECommerceApplication.Commands;

public class CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}