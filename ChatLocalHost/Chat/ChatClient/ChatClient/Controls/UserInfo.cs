using System.Windows.Forms;
using ChatClient.Utils;

namespace ChatClient.Controls
{
    public partial class UserInfo : UserControl
    { 
        
        public UserInfo()
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
    }
}
