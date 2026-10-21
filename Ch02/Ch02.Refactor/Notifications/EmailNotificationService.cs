namespace EduHub.Ch02.Notifications;

public sealed class EmailNotificationService : INotificationService
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"Email to {recipient}: {message}");
    }
}
