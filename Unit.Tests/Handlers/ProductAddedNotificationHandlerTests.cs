using MediatRNotificationExample.Handlers.Products;
using MediatRNotificationExample.Interfaces;
using MediatRNotificationExample.Notifications;
using NSubstitute;

namespace Unit.Tests.Handlers;

[TestFixture]
public class ProductAddedNotificationHandlerTests
{
    private IExternalProductSyncService _externalProdutSyncService;
    
    private ProductAddedNotificationHandler _handler;

    [SetUp]
    public void Setup()
    {
        _externalProdutSyncService = Substitute.For<IExternalProductSyncService>();

        _handler = new ProductAddedNotificationHandler(_externalProdutSyncService);
    }

    [Test]
    public async Task Should_BeginExternalSync()
    {
        // Given
        const string sku = "12345";
        var notification = new ProductAddedNotification(sku);

        // When
        await _handler.Handle(notification, CancellationToken.None);

        // Then
        await _externalProdutSyncService.Received().SyncProductBySku(sku);
    }
}