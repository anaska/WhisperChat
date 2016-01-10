using System;
using System.Windows.Forms;
using ChatClient.Utils;

namespace ChatClient.Controls
{
    public partial class FriendRequest : UserControl
    {
        public FriendRequest()
        {
            InitializeComponent();
        }

        public ChatManager Chat { get; set; }

        public void Init()
        {
            lblName.Text = Chat.CurrentUser.Name;
            lblStatus.Text = Chat.CurrentUser.Connected ? "Online" : "Offline";
            lblStatus.Image = Chat.CurrentUser.Connected ? Properties.Resources.on :
                        Properties.Resources.off;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Chat.Server.FriendRequest(Chat.CurrentUser, Chat.Me, global::Chat.RequestType.Accept);
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            Chat.Server.FriendRequest(Chat.CurrentUser, Chat.Me, global::Chat.RequestType.Reject);
        }
    }
}
