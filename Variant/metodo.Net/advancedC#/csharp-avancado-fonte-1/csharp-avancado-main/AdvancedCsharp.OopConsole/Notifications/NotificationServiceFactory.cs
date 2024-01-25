using System;

namespace AdvancedCsharp.OopConsole.Notifications
{
	public static class NotificationServiceFactory
	{
        public static INotificationService CreateNotificationService(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Push:
                    return new PushNotificationService();
                case NotificationType.Sms:
                    return new SmsService();
                case NotificationType.Email:
                    return new EmailService();
                default:
                    throw new ArgumentException("Invalid notification type");
            }
        }
    }
}

