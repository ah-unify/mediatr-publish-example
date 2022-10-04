using MediatR;
using MediatRNotificationExample.Interfaces;
using MediatRNotificationExample.Notifications.Interfaces;

namespace MediatRNotificationExample.Handlers;

public class EmailProductManagementNotificationHandler<T> : INotificationHandler<T>
    where T : IEmailProductManagementNotification
{
    private readonly IEmailer _emailer;
    private readonly IEmailAddressResolver _emailAddressResolver;

    public EmailProductManagementNotificationHandler(IEmailer emailer,
        IEmailAddressResolver emailAddressResolver)
    {
        _emailer = emailer;
        _emailAddressResolver = emailAddressResolver;
    }

    public async Task Handle(T notification, CancellationToken cancellationToken)
    {
        var address = await _emailAddressResolver.GetProductManagementEmailAddress();
        await _emailer.SendTemplatedEmail(address, notification.EmailTemplate);
    }
}