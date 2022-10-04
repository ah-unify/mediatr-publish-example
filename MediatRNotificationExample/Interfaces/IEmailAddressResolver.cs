namespace MediatRNotificationExample.Interfaces;

public interface IEmailAddressResolver
{
    public Task<string> GetProductManagementEmailAddress();
}