using Chat;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ChatServer.Dao
{
    public class Client : MarshalByRefObject, IClient
    {
        public Client(int id, string name, bool status)
        {
            ID = id;
            Name = name.TrimEnd();
            Connected = status;
        }

        public bool Connected { get; set; }

        public List<IClient> Friends { get; set; } = new List<IClient>();

        public int ID { get; set; }

        public string Name { get; set; }

        public List<NotificationContainer> NotificationQueue { get; set; } = new List<NotificationContainer>();

        public IDictionary Notification { get; set; } = new Hashtable();


        public void AddNotification(int id, bool b)
        {
            lock (Notification)
            {
                if (!Notification.Contains(id))
                    Notification.Add(id, b);
                else
                    Notification[id] = b;
            }
        }

        public void AddNotificationQueue(NotificationContainer noti)
        {
            lock (NotificationQueue)
            {
                NotificationQueue.Add(noti);
            }
        }

        public void ClearNotificationQueue()
        {
            lock (NotificationQueue)
            {
                NotificationQueue.Clear();
            }
        }

        public void RemoveNotification(int id)
        {
            lock (Notification)
            {
                Notification.Remove(id);
            }
        }

        public void AddFriend(IClient client)
        {
            lock (Friends)
            {
                Friends.Add(client);
            }
        }
    }
}
