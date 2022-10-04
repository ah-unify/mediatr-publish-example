using MediatRNotificationExample.Interfaces;

namespace MediatRNotificationExample.Infrastructure;

public class Emailer : IEmailer
{
    public Task SendTemplatedEmail(string emailAddress, string template)
    {
        Console.WriteLine($"Sending email with template {template} to address {emailAddress}");
        return Task.CompletedTask;
    }
}