using Chat;
using System;

namespace ChatClient.Utils
{
    public class MessageEventArgs :EventArgs
    {
        public MessageEventArgs(IClient client,int id,string msg)
        {
            Message = msg;
            Client = client;
            ID = id;
            TimeStamp = DateTime.Now;
        }

        public IClient Client { get; private set; }
        public string Message { get; private set; }        
        public int ID { get; private set; }  

        public DateTime TimeStamp { get; private set; }
    }

    public class FriendRequestEventArgs : EventArgs
    {
        public FriendRequestEventArgs(IClient client)
        {
            Client = client;
        }
        public IClient Client { get; private set; }
    }  

    public class StatusChangeEventArgs : EventArgs
    {
        public StatusChangeEventArgs(IClient client,bool status)
        {
            Client = client;
            Status = status;
        }

        public IClient Client { get; private set; }
        public bool Status { get; private set; }
    }
}
