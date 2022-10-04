namespace MediatRNotificationExample.Notifications.Interfaces;

public interface IEmailProductManagementNotification : ISkuNotification
{
    public string EmailTemplate { get; }
}