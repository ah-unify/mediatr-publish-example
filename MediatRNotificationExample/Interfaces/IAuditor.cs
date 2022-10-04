namespace MediatRNotificationExample.Interfaces;

public interface IAuditor
{
    public Task Audit(string sku, string auditMsg);
}