namespace AdvancedCsharp.OopConsole.Notifications;
public interface INotificationService
{
    void SendNotification(string message);
    string Format(string message);
}
