using System.Collections;
using System.Collections.Generic;

namespace Chat
{    
    public interface IClient
    {
        int ID { get; set; }
        string Name { get; set; }

        List<IClient> Friends { get; set; }

        void AddFriend(IClient client);
     
        bool Connected { get; set; }   

        IDictionary Notification { get; set; }

        void AddNotification(int id, bool b);
        void RemoveNotification(int id);
        List<NotificationContainer> NotificationQueue { get;set; }

        void ClearNotificationQueue();       
        void AddNotificationQueue(NotificationContainer noti);
    }    
}
