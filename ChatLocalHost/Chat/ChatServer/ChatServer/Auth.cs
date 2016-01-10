using System;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Security.Principal;

namespace ChatServer
{
    class Auth : IAuthorizeRemotingConnection
    {
        public bool IsConnectingEndPointAuthorized(EndPoint endPoint)
        {                
            return true;
        }

        public bool IsConnectingIdentityAuthorized(IIdentity identity)
        {
            Console.WriteLine("Identity:" +identity.Name);
            return true;
        }
    }
}
