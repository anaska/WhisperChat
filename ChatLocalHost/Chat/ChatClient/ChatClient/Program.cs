using System;
using System.Windows.Forms;
using Chat;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using ChatClient.Utils;

namespace ChatClient
{
    static class Program
    {
        public static IClient Client { get; set; }

        public static IChatServer Server { get; private set; }

        public enum State { MainForm, LoginForm, Quit };
        public static State CurrentState { get; set; } = State.LoginForm;

        public static bool ValidLogin { get; set; } = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Init();
            while (true)
            {
                switch (CurrentState)
                {
                    case State.MainForm:
                        Application.Run(new MainForm());
                        break;
                    case State.LoginForm:
                        Application.Run(new LoginForm());
                        break;
                    case State.Quit:
                        return;
                }
            }
        }

        private static void Init()
        {
            RemotingConfiguration.Configure("ChatClient.exe.config", false);

            Server = (IChatServer)Activator.GetObject(
                typeof(IChatServer),
                "tcp://192.168.175.1:8001/chat");

            IDictionary props = ChannelServices.GetChannelSinkProperties(Server);
            props["username"] = "test";
            props["password"] = "password";
        }
    }
}
