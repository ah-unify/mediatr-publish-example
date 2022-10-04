namespace MediatRNotificationExample.Interfaces;

public interface IEmailer
{
    public Task Email(string emailAddress, string template);
}