using MediatRNotificationExample.Handlers;
using MediatRNotificationExample.Interfaces;
using MediatRNotificationExample.Notifications.Interfaces;
using NSubstitute;

namespace Unit.Tests.Handlers;

[TestFixture]
public class EmailProductManagementNotificationHandlerTests
{
    private IEmailer _emailer;
    private IEmailAddressResolver _emailAddressResolver;
    private EmailProductManagementNotificationHandler<TestEmailNotification> _handler;

    [SetUp]
    public void Setup()
    {
        _emailer = Substitute.For<IEmailer>();
        _emailAddressResolver = Substitute.For<IEmailAddressResolver>();

        _handler = new EmailProductManagementNotificationHandler<TestEmailNotification>(_emailer,
            _emailAddressResolver);
    }

    [Test]
    public async Task Should_EmailProductManagement_With_Sku()
    {
        // Given
        var notification = new TestEmailNotification();
        const string email = "angus.hankinson@unifysolutions.net";
        
        _emailAddressResolver.GetProductManagementEmailAddress().Returns(email);
        
        // When
        await _handler.Handle(notification, CancellationToken.None);

        // Then
        await _emailer.Received().SendTemplatedEmail(email, notification.EmailTemplate);
    }
}

public class TestEmailNotification : IEmailProductManagementNotification
{
    public string Sku => "12345";
    public string EmailTemplate => "test_email_template";
}