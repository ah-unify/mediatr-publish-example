using MediatRNotificationExample.Interfaces;

namespace MediatRNotificationExample.Infrastructure;

public class Emailer : IEmailer
{
    public Task Email(string emailAddress, string template)
    {
        Console.WriteLine($"Sending email with template {template} to address {emailAddress}");
        return Task.CompletedTask;
    }
}