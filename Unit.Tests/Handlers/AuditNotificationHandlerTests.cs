using MediatRNotificationExample.Handlers;
using MediatRNotificationExample.Interfaces;
using MediatRNotificationExample.Notifications.Interfaces;
using NSubstitute;

namespace Unit.Tests.Handlers;

[TestFixture]
public class AuditNotificationHandlerTests
{
    private IAuditor _auditor;
    private AuditNotificationHandler<TestAuditNotification> _handler;

    [SetUp]
    public void Setup()
    {
        _auditor = Substitute.For<IAuditor>();
        
        _handler = new AuditNotificationHandler<TestAuditNotification>(_auditor);
    }

    [Test]
    public async Task Should_AuditAgainstSkuWithAuditTemplate()
    {
        // Given
        var notification = new TestAuditNotification();
        // When
        await _handler.Handle(notification, CancellationToken.None);

        // Then
        await _auditor.Received().Audit(notification.Sku, notification.AuditTemplate);
    }
}

public class TestAuditNotification : IAuditNotification
{
    public string Sku => "12345";
    public string AuditTemplate => "test_audit";
}