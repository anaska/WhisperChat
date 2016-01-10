using System;
using System.Collections.Generic;
using System.Runtime.Remoting;

namespace Chat
{

    public enum MessageType { Write, Read, History };
    public enum RequestType { FriendRequests, Accept, Reject, Remove };
    public interface IChatServer
    {
        IClient Login(string user, string pass);
        void Logout(IClient client);

        bool Register(string user, string pass);

        List<MessageContainer> Message(IClient from, IClient to, MessageType type, object obj = null);

        int GetFriendsCount(IClient client);
        List<IClient> GetFriends(IClient client);
        List<IClient> GetAllUsers();

        void FriendRequest(IClient from, IClient to, RequestType type);
        List<IClient> FriendRequest(IClient client);

        List<IClient> SearchUser(string name, IClient client);
    }

    [Serializable]
    public class InvalidUserException : RemotingException
    {
        public InvalidUserException(string msg) : base(msg)
        {

        }
    }
}
