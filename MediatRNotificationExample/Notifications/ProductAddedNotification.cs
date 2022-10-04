using MediatRNotificationExample.Notifications.Interfaces;

namespace MediatRNotificationExample.Notifications;

public class ProductAddedNotification : IAuditNotification, IEmailProductManagementNotification
{
    public string Sku { get; }
    public string AuditTemplate => "product_added_audit";
    public string EmailTemplate => "product_added_template";

    public ProductAddedNotification(string sku)
    {
        Sku = sku;
    }
}