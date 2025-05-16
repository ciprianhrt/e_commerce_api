using MediatR;

namespace ECommerceApplication.Commands;

public class DeleteProductCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}