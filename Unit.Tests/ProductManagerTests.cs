using MediatR;
using MediatRNotificationExample;
using MediatRNotificationExample.Domain;
using MediatRNotificationExample.Interfaces;
using MediatRNotificationExample.Notifications;
using NSubstitute;

namespace Unit.Tests;

public class ProductManagerTests
{
    private IProductRepository _productRepository;
    private IMediator _mediator;

    private ProductManager _productManager;

    [SetUp]
    public void Setup()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mediator = Substitute.For<IMediator>();

        _productManager = new ProductManager(_productRepository,
            _mediator);
    }

    [Test]
    public void Should_AddProduct_And_RaiseNotification()
    {
        // Given
        const string sku = "12345";
        const string title = "OverpricedGpu";

        // When
        _productManager.AddProduct(sku, title);

        // Then
        _productRepository.Received().Add(Arg.Is<Product>(x => x.Sku == sku && x.Title == title));
        _mediator.Received().Publish(Arg.Is<ProductAddedNotification>(x => x.Sku == sku));
    }

    [Test]
    public void Should_UpdateProduct_And_RaiseNotification()
    {
        // Given an existing product
        const string sku = "12345";
        const string title = "OverpricedGpu";
        _productRepository.Get(sku).Returns(new Product(sku, title));

        const string newTitle = "RegularlyPricedGpu";

        // When
        _productManager.UpdateProduct(sku, newTitle);

        // Then
        _productRepository.Received().Update(Arg.Is<Product>(x => x.Sku == sku && x.Title == newTitle));
        _mediator.Received().Publish(Arg.Is<ProductUpdatedNotification>(x => x.Sku == sku));
    }

    [Test]
    public void Should_RemoveProduct_And_RaiseNotification()
    {
        // Given
        const string sku = "12345";

        // When
        _productManager.RemoveProduct(sku);

        // Then
        _productRepository.Received().Remove(sku);
        _mediator.Received().Publish(Arg.Is<ProductRemovedNotification>(x => x.Sku == sku));
    }
}