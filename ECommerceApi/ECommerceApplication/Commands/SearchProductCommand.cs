using ECommerceDomain;
using MediatR;

namespace ECommerceApplication.Commands;

public class SearchProductCommand : IRequest<List<Product>>
{
    public Guid? Id { get; set; }
    public string? ProductName { get; set; }
}