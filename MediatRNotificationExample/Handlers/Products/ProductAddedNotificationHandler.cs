using MediatR;
using MediatRNotificationExample.Interfaces;
using MediatRNotificationExample.Notifications;

namespace MediatRNotificationExample.Handlers.Products;

public class ProductAddedNotificationHandler : INotificationHandler<ProductAddedNotification>
{
    private readonly IExternalProductSyncService _externalProductSyncService;

    public ProductAddedNotificationHandler(IExternalProductSyncService externalProductSyncService)
    {
        _externalProductSyncService = externalProductSyncService;
    }
    
    public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
    {
        await _externalProductSyncService.SyncProductBySku(notification.Sku);
    }
}