namespace MediatRNotificationExample.Interfaces;

public interface IExternalProductSyncService
{
    public Task SyncProductBySku(string sku);
}