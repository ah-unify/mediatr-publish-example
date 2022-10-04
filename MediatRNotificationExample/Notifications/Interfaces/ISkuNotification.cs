using MediatR;

namespace MediatRNotificationExample.Notifications.Interfaces;

public interface ISkuNotification : INotification
{
    public string Sku { get; }
}