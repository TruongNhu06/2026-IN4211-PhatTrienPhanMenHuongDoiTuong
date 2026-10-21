namespace EduHub.Ch02.Notifications;

public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(NotificationChannel channel)
    {
        switch (channel)
        {
            case NotificationChannel.Email:
                return new EmailNotificationService();

            case NotificationChannel.Sms:
                return new SmsNotificationService();

            case NotificationChannel.Slack:
                return new SlackNotificationService();

            default:
                throw new ArgumentOutOfRangeException(nameof(channel), channel, "Thông báo không được hổ trợ");
        }
    }
}