using ChatServer.Dao;
using System;
using System.Runtime.Remoting;
using ChatServer.Service;


namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("ChatServer.exe.config",false);
            DB.UpdateStatusOfflineAll();   
            Console.ReadLine();           
        }
    }
}
