using MediatRNotificationExample.Interfaces;

namespace MediatRNotificationExample.Infrastructure;

public class Auditor : IAuditor
{
    public Task Audit(string sku, string auditMsg)
    {
        Console.WriteLine($"Audit for sku: {sku}, Message: {auditMsg}");
        return Task.CompletedTask;
    }
}