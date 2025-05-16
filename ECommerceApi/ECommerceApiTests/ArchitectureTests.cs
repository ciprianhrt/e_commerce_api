namespace ECommerceApiTests;

using NetArchTest.Rules;
using Xunit;
using MediatR;

public class ArchitectureTests
{
    [Fact]
    public void Handlers_Should_Implement_IRequestHandler()
    {
        var result = Types.InAssembly(typeof(ECommerceApplication.Handlers.CreateProductHandler).Assembly)
            .That()
            .ResideInNamespace("ECommerceApplication.Handlers")
            .Should()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .GetResult();

        Assert.True(result.IsSuccessful, "All handlers should implement IRequestHandler<TRequest, TResponse>");
    }

    [Fact]
    public void Commands_Should_Implement_IRequest()
    {
        var result = Types.InAssembly(typeof(ECommerceApplication.Commands.CreateProductCommand).Assembly)
            .That()
            .ResideInNamespace("ECommerceApplication.Commands")
            .Should()
            .ImplementInterface(typeof(IRequest<>))
            .GetResult();

        Assert.True(result.IsSuccessful, "All commands should implement IRequest<TResponse>");
    }

    [Fact]
    public void Handlers_Should_Not_Reference_Controllers()
    {
        var result = Types.InAssembly(typeof(ECommerceApplication.Handlers.CreateProductHandler).Assembly)
            .That()
            .ResideInNamespace("ECommerceApplication.Handlers")
            .ShouldNot()
            .HaveDependencyOn("ECommerceAPI.Controllers")
            .GetResult();

        Assert.True(result.IsSuccessful, "Handlers should not depend on controllers (violates CQRS separation)");
    }
}
