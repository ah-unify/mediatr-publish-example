using MediatRNotificationExample.Notifications.Interfaces;

namespace MediatRNotificationExample.Notifications;

public class ProductUpdatedNotification : IAuditNotification
{
    public string Sku { get; }
    public string AuditMessage => "product_updated_audit";

    public ProductUpdatedNotification(string sku)
    {
        Sku = sku;
    }
}