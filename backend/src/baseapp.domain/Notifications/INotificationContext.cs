using System.Collections.Generic;

namespace BaseApp.Domain.Notifications
{
    public interface INotificationContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }

        bool HasNotification { get; }

        void AddNotification(string key, string message);

        void AddNotification(Notification notification);

        void AddNotifications(IList<Notification> notifications);
    }
}