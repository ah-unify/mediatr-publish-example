namespace MediatRNotificationExample.Notifications.Interfaces;

public interface IAuditNotification : ISkuNotification
{
    public string AuditTemplate { get; }
}