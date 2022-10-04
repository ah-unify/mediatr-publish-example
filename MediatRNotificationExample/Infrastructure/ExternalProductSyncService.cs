using MediatRNotificationExample.Interfaces;

namespace MediatRNotificationExample.Infrastructure;

public class ExternalProductSyncService : IExternalProductSyncService
{
    public Task SyncProductBySku(string sku)
    {
        Console.WriteLine($"Sending external sync request for product SKU: {sku}");
        return Task.CompletedTask;
    }
}