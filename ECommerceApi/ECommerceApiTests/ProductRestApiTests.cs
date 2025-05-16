using ECommerceApplication.Commands;
using ECommerceApplication.Handlers;
using ECommerceApplication.Repositories;
using ECommerceDomain;
using Moq;
using Xunit;

namespace ECommerceApiTests;

public class ProductRestApiTests
{
    [Fact]
    public async Task Handle_ShouldCreateProductAndReturnIt()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.AddProductAsync(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateProductHandler(mockRepo.Object);
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Price = 99.99m
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.Price, result.Price);
        Assert.NotEqual(Guid.Empty, result.Id);

        mockRepo.Verify(repo => repo.AddProductAsync(It.Is<Product>(
            p => p.Name == command.Name && p.Price == command.Price)), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldCallDeleteProductAsync_AndReturnProductId()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var mockRepo = new Mock<IProductRepository>();

        mockRepo.Setup(r => r.DeleteProductAsync(productId)).Returns(Task.CompletedTask);

        var handler = new DeleteProductHandler(mockRepo.Object);
        var command = new DeleteProductCommand { Id = productId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(productId, result);
        mockRepo.Verify(r => r.DeleteProductAsync(productId), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WithProductName_ShouldCallSearchByName()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var expectedProducts = new List<Product> { new Product { Name = "Test" } };
        
        mockRepo.Setup(r => r.SearchProductByName("Test")).ReturnsAsync(expectedProducts);
        
        var handler = new SearchProductHandler(mockRepo.Object);
        var command = new SearchProductCommand { ProductName = "Test" };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedProducts, result);
        mockRepo.Verify(r => r.SearchProductByName("Test"), Times.Once);
    }

    [Fact]
    public async Task Handle_WithId_ShouldCallSearchById()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedProduct = new Product { Id = id, Name = "Test" };

        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(r => r.SearchProductByIdAsync(id)).ReturnsAsync(expectedProduct);

        var handler = new SearchProductHandler(mockRepo.Object);
        var command = new SearchProductCommand { Id = id };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Single(result);
        Assert.Equal(expectedProduct, result[0]);
        mockRepo.Verify(r => r.SearchProductByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task Handle_WithoutIdOrName_ShouldCallSearchAll()
    {
        // Arrange
        var expectedProducts = new List<Product> {
            new Product { Name = "Prod1" },
            new Product { Name = "Prod2" }
        };

        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(r => r.SearchProductsAsync()).ReturnsAsync(expectedProducts);

        var handler = new SearchProductHandler(mockRepo.Object);
        var command = new SearchProductCommand(); // No filters

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedProducts, result);
        mockRepo.Verify(r => r.SearchProductsAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingId_ShouldReturnEmptyList()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(r => r.SearchProductByIdAsync(id)).ReturnsAsync((Product?)null);

        var handler = new SearchProductHandler(mockRepo.Object);
        var command = new SearchProductCommand { Id = id };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Empty(result);
        mockRepo.Verify(r => r.SearchProductByIdAsync(id), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldUpdateProductAndReturnUpdatedProduct()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var productId = Guid.NewGuid();

        mockRepo.Setup(r => r.UpdateProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

        var handler = new UpdateProductHandler(mockRepo.Object);
        var command = new UpdateProductCommand
        {
            Id = productId,
            Name = "Updated Product",
            Price = 199.99m
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Id, result.Id);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.Price, result.Price);

        mockRepo.Verify(r => r.UpdateProductAsync(It.Is<Product>(
            p => p.Id == command.Id && p.Name == command.Name && p.Price == command.Price
        )), Times.Once);
    }
}