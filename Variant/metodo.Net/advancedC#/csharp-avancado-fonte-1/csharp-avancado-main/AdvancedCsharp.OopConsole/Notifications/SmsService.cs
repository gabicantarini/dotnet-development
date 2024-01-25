namespace AdvancedCsharp.OopConsole.Notifications;

public class SmsService : NotificationService
{
    public override void SendNotification(string message)
    {
        Console.WriteLine($"Sending SMS: {message}");
    }
}
