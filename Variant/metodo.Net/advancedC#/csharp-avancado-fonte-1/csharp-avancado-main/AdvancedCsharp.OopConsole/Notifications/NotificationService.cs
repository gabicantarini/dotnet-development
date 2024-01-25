namespace AdvancedCsharp.OopConsole.Notifications;

public abstract class NotificationService : INotificationService
{
    public string Format(string message)
    {
        return message.Trim();
    }

    public abstract void SendNotification(string message);
}
