using System;

namespace Chat
{
    public enum NotificationType { Message, StatusUpdate, FriendRequest, AcceptRequest };
    [Serializable]
    public class NotificationContainer
    {

        public NotificationContainer(IClient client, string msg, int id)
        {
            Value = msg;
            Client = client;
            ID = id;
            Type = NotificationType.Message;
            TimeStamp = DateTime.Now;
        }

        public NotificationContainer(IClient client, bool status)
        {
            Value = status;
            Client = client;
            Type = NotificationType.StatusUpdate;
            TimeStamp = DateTime.Now;
        }

        public NotificationContainer(IClient client)
        {
            Client = client;
            Type = NotificationType.FriendRequest;
            TimeStamp = DateTime.Now;
        }

        public NotificationContainer(IClient client, NotificationType type)
        {
            Client = client;
            Type = type;
            TimeStamp = DateTime.Now;
        }

        public IClient Client { get; private set; }
        public object Value { get; private set; }

        public int ID { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public NotificationType Type { get; private set; }

    }
}
