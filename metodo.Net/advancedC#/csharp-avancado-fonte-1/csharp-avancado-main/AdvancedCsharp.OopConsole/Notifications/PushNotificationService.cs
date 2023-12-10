namespace AdvancedCsharp.OopConsole.Notifications;

public class PushNotificationService : NotificationService, INotificationService
{
    public override void SendNotification(string message)
    {
        Console.WriteLine($"Sending push notification: {message}");
    }
}
