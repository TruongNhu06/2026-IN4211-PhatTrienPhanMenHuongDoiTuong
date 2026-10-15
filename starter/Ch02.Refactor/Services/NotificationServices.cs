namespace EduHub.Ch02;

public sealed class EmailNotificationService : INotificationService
{
    public void Notify(string recipient, string message) => Console.WriteLine($"Email to {recipient}: {message}");
}

public sealed class SlackNotificationService : INotificationService
{
    public void Notify(string recipient, string message) => Console.WriteLine($"Slack to {recipient}: {message}");
}

public sealed class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(NotificationChannel channel) => channel switch
    {
        NotificationChannel.Email => new EmailNotificationService(),
        NotificationChannel.Slack => new SlackNotificationService(),
        _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, "Unsupported notification channel")
    };
}
