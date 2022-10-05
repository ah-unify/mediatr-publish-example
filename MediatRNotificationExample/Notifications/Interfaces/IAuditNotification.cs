namespace MediatRNotificationExample.Notifications.Interfaces;

public interface IAuditNotification : ISkuNotification
{
    public string AuditMessage { get; }
}