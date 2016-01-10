using System;
using System.Windows.Forms;
using ChatClient.Utils;
using Chat;

namespace ChatClient.Controls
{
    public partial class FriendSearch : UserControl
    {       
        public FriendSearch()
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
            btnAccept.Show();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Chat.Server.FriendRequest(Chat.Me, Chat.CurrentUser, RequestType.FriendRequests);
            btnAccept.Hide();
        }
    }
}
