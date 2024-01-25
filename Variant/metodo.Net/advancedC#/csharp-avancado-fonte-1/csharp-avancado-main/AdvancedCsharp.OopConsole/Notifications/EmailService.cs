namespace AdvancedCsharp.OopConsole.Notifications;

public class EmailService : NotificationService
{
    public override void SendNotification(string message)
    {
        Console.WriteLine($"Sending email: {message}");
    }
}
