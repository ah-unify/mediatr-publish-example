namespace MediatRNotificationExample.Interfaces;

public interface IEmailer
{
    public Task SendTemplatedEmail(string emailAddress, string template);
}