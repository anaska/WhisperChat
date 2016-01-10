using Chat;
using ChatServer.Dao;
using System;
using System.Collections.Generic;
using System.Net;

namespace ChatServer.Service
{
    public class Server : MarshalByRefObject, IChatServer
    {
        private static List<IClient> onlineUsers = new List<IClient>();
        private static DB db = new DB();

        private static bool IsOnline(IClient client)
        {
            foreach (IClient c in onlineUsers)
                if (c.ID == client.ID)
                    return true;
            return false;
        }

        private static void AddUser(IClient c)
        {
            if (!IsOnline(c))
            {
                db.UpdateStatus(c.ID, true);
                c.Connected = true;
                lock (onlineUsers)
                {
                    onlineUsers.Add(c);
                }
                foreach (IClient friend in c.Friends)
                    if (IsOnline(friend))
                        friend.AddNotificationQueue(new NotificationContainer(c, true));

            }
        }

        private static void RemoveUser(IClient c)
        {
            if (IsOnline(c))
            {
                db.UpdateStatus(c.ID, false);
                c.Connected = false;
                lock (onlineUsers)
                {
                    onlineUsers.Remove(c);
                }
                foreach (IClient friend in c.Friends)
                    if (IsOnline(friend))
                        friend.AddNotificationQueue(new NotificationContainer(c, false));
            }
        }

        private void GetNotification(IClient c)
        {
            foreach (IClient friend in c.Friends)
                if (db.GotMessage(friend.ID, c.ID))
                    c.AddNotification(friend.ID, true);
        }

        public static IClient User(int id)
        {
            foreach (IClient c in onlineUsers)
            {
                if (c.ID == id)
                    return c;
                foreach (IClient c2 in c.Friends)
                    if (c2.ID == id)
                        return c2;
            }
            return null;
        }

        public void FriendRequest(IClient from, IClient to, RequestType type)
        {
            switch (type)
            {
                case RequestType.FriendRequests:
                    db.FriendRequest(from.ID, to.ID);
                    if (IsOnline(to))
                        to.AddNotificationQueue(new NotificationContainer(from));
                    return;
                case RequestType.Accept:
                    db.AddFriend(from.ID, to.ID, true);
                    if (IsOnline(to))
                        to.AddNotificationQueue(new NotificationContainer(from,
                            NotificationType.AcceptRequest));
                    return;
                case RequestType.Reject:
                    db.AddFriend(from.ID, to.ID, false);
                    return;
                case RequestType.Remove:
                    break;
            }
        }

        public List<IClient> FriendRequest(IClient client)
        {
            return db.FriendRequest(client.ID);
        }

        public List<IClient> GetFriends(IClient client)
        {
            return db.GetFriends(client.ID);
        }

        public List<IClient> GetAllUsers()
        {
            return db.GetAllUsers();
        }

        public int GetFriendsCount(IClient client)
        {
            return db.FriendRequestCount(client.ID);
        }

        public string GetIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            Console.WriteLine(ipAddress.ToString());
            return ipAddress.ToString();
        }

        public IClient Login(string user, string pass)
        {
            try
            {
                IClient client = db.User(user, pass);
                client.Friends = GetFriends(client);
                GetNotification(client);
                AddUser(client);                
                return client;
            }
            catch (InvalidUserException ex)
            {
                throw ex;
            }
        }

        public void Logout(IClient client)
        {
            RemoveUser(client);
        }

        public List<MessageContainer> Message(IClient from, IClient to, MessageType type, object obj)
        {
            switch (type)
            {                
                case MessageType.Write:
                    if ((string)obj != null)
                    {
                        int id = db.AddMessage(from.ID, to.ID, (string)obj);
                        if (IsOnline(to))
                            to.AddNotificationQueue(new NotificationContainer(from, (string)obj, id));
                    }
                    break;
                case MessageType.Read:
                    return db.GetMessage(from, to, true);
                case MessageType.History:
                    return db.GetHistory(from, to, (int)obj);

            }
            return null;
        }

        public bool Register(string user, string pass)
        {
            return db.Register(user, pass);
        }

        public List<IClient> SearchUser(string name, IClient client)
        {
            return db.SearchUser(name, client.ID);
        }
    }
}
