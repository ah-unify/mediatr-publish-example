using MediatRNotificationExample.Interfaces;

namespace MediatRNotificationExample.Infrastructure;

public class EmailAddressResolver : IEmailAddressResolver
{
    public Task<string> GetProductManagementEmailAddress()
    {
        return Task.FromResult("angus.hankinson@unifysolutions.net");
    }
}