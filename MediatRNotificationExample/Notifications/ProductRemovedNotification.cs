using MediatRNotificationExample.Notifications.Interfaces;

namespace MediatRNotificationExample.Notifications;

public class ProductRemovedNotification : IAuditNotification, IEmailProductManagementNotification
{
    public string Sku { get; }
    public string AuditMessage => "product_removed_audit";
    public string EmailTemplate => "product_removed_template";

    public ProductRemovedNotification(string sku)
    {
        Sku = sku;
    }
}