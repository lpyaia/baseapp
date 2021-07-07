using BaseApp.Domain.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace BaseApp.Infra.CrossCutting.Notification
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Domain.Notifications.Notification> _notifications;

        public IReadOnlyCollection<Domain.Notifications.Notification> Notifications => _notifications;

        public bool HasNotification => _notifications.Any();

        public NotificationContext()
        {
            _notifications = new List<Domain.Notifications.Notification>();
        }

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new Domain.Notifications.Notification(key, message));
        }

        public void AddNotification(Domain.Notifications.Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IList<Domain.Notifications.Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }
    }
}