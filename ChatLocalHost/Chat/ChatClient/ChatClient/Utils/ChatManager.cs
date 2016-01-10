using Chat;
using ChatClient.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient.Utils
{
    public class ChatManager
    {

        public ChatManager()
        {
            Me = Program.Client;
            Server = Program.Server;
            ChatMemory = new ChatMemory();
        }


        public event EventHandler<MessageEventArgs> OnMessage;
        public event EventHandler<FriendRequestEventArgs> OnFriendRequest;
        public event EventHandler<FriendRequestEventArgs> OnAcceptFriend;
        public event EventHandler<StatusChangeEventArgs> OnStatusChanged;

        public IClient Me { get; private set; }
        public IClient CurrentUser { get; set; }
        public IChatServer Server { get; private set; }
        public List<IClient> Friends { get { return Me.Friends; } }
        public List<IClient> AllUsers { get { return Server.GetAllUsers(); } }

        public bool IsRunning { get; private set; } = false;

        public ChatMemory ChatMemory { get; private set; }



        public void StartWorker()
        {
            if (Me == null)
                throw new Exception("You are need to provide client.");
            if (IsRunning)
            {
                IsRunning = false;
                Thread.Sleep(40);
            }
            IsRunning = true;
            Run();
        }

        public void StopWorker()
        {
            IsRunning = false;
        }


        private void Run()
        {
            Task.Run(() =>
            {
                try
                {
                    while (IsRunning)
                    {
                        if (Me.NotificationQueue.Count > 0)
                        {

                            foreach (NotificationContainer n in Me.NotificationQueue)
                            {
                                switch (n.Type)
                                {
                                    case NotificationType.AcceptRequest:
                                        if (OnAcceptFriend != null)
                                            OnAcceptFriend(this, new FriendRequestEventArgs(n.Client));
                                        break;
                                    case NotificationType.FriendRequest:
                                        if (OnFriendRequest != null)
                                            OnFriendRequest(this, new FriendRequestEventArgs(n.Client));
                                        break;
                                    case NotificationType.Message:
                                        if (OnMessage != null)
                                            OnMessage(this, new MessageEventArgs(n.Client, n.ID, (string)n.Value));
                                        break;
                                    case NotificationType.StatusUpdate:
                                        if (OnStatusChanged != null)
                                            OnStatusChanged(this, new StatusChangeEventArgs(n.Client, (bool)n.Value));
                                        break;
                                }
                            }
                            lock (Me.NotificationQueue)
                            {
                                Me.ClearNotificationQueue();
                            }
                        }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception)
                {
                    Run();                    
                }
            });
        }

    }
}
