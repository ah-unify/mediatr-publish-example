using MediatR;
using MediatRNotificationExample.Interfaces;
using MediatRNotificationExample.Notifications.Interfaces;

namespace MediatRNotificationExample.Handlers;

public class AuditNotificationHandler<T> : INotificationHandler<T> where T : IAuditNotification
{
    private readonly IAuditor _auditor;

    public AuditNotificationHandler(IAuditor auditor)
    {
        _auditor = auditor;
    }
    
    public async Task Handle(T notification, CancellationToken cancellationToken)
    {
        await _auditor.Audit(notification.Sku, notification.AuditMessage);
    }
}