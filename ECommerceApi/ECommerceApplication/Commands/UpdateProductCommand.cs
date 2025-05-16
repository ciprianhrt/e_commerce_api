using ECommerceDomain;
using MediatR;

namespace ECommerceApplication.Commands;

public class UpdateProductCommand : IRequest<Product>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}