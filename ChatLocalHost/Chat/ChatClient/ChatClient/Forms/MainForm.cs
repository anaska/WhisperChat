using System.Windows.Forms;
using System;
using Chat;
using ChatClient.Utils;
using ChatClient.Controls;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace ChatClient
{
    public partial class MainForm : Form
    {
        private bool closing = true;
        private State state;
        private ChatManager chat;
        private FriendRequest friendRequest = new FriendRequest();
        private UserInfo userInfo = new UserInfo();
        private FriendSearch friendSearch = new FriendSearch();

        private enum State { SheachingFriend, Request, None }

        public MainForm()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            state = State.None;

            chat = new ChatManager();
            chat.StartWorker();
            chat.OnMessage += Chat_OnMessage;
            chat.OnStatusChanged += Chat_OnStatusChanged;
            chat.OnFriendRequest += Chat_OnFriendRequest;
            chat.OnAcceptFriend += Chat_OnAcceptFriend;

            Text = "Chat - " + chat.Me.Name;

            friendRequest.Chat = chat;
            userInfo.Chat = chat;
            friendSearch.Chat = chat;
            userList1.DataSource = chat.Friends;
            Chat_OnFriendRequest(null, null);
            ShowInput(false);
        }

        private void ShowInput(bool b)
        {
            if (b)
            {
                txtSend.Visible = true;
                btnSend.Visible = true;
                rtbInput.Visible = true;
                return;
            }
            txtSend.Visible = false;
            btnSend.Visible = false;
            rtbInput.Visible = false;
        }

        private void Chat_OnAcceptFriend(object sender, FriendRequestEventArgs e)
        {
            chat.Me.AddFriend(e.Client);
            if (e.Client.Connected)
                e.Client.AddNotificationQueue(new NotificationContainer(chat.Me,
                    NotificationType.AcceptRequest));
            Chat_OnFriendRequest(null, null);
        }


        private void UpdateRequestFriend(int nr)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateRequestFriend(nr)));
            }
            else
                lblFriend.Text = nr.ToString();
        }

        private void Chat_OnFriendRequest(object sender, FriendRequestEventArgs e)
        {
            UpdateRequestFriend(chat.Server.GetFriendsCount(chat.Me));
        }

        private void Chat_OnStatusChanged(object sender, StatusChangeEventArgs e)
        {
            userList1.UpdateItem(e.Client);
        }

        private void Chat_OnMessage(object sender, MessageEventArgs e)
        {
            if (chat.CurrentUser != null && e.Client.ID == chat.CurrentUser.ID)
            {
                var arr = chat.Server.Message(chat.CurrentUser, chat.Me, MessageType.Read);
                foreach (MessageContainer msc in arr)
                    chat.ChatMemory.Add(chat.CurrentUser.ID, msc);
                UpdateInput();
                return;
            }
            chat.Me.AddNotification(e.Client.ID, true);
            userList1.UpdateItem(e.Client);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closing)
                Program.CurrentState = Program.State.Quit;
            chat.StopWorker();
            chat.Server.Logout(chat.Me);
        }

        private void tsmSighOut_Click(object sender, System.EventArgs e)
        {
            closing = false;
            Program.CurrentState = Program.State.LoginForm;
            Close();
        }

        private void tsmClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSend.Text != txtSend.DefaultValue)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                UpdateInput(txtSend.Text);
            }
        }

        private void UpdateInput(string text)
        {
            if (chat.CurrentUser != null && text != "")
            {
                MessageContainer msc = new MessageContainer();
                msc.From = chat.Me;
                msc.To = chat.CurrentUser;
                msc.Message = txtSend.Text;
                msc.TimeStamp = DateTime.Now;

                chat.ChatMemory.Add(chat.CurrentUser.ID, msc);
                chat.Server.Message(chat.Me, chat.CurrentUser, MessageType.Write, msc.Message);
                UpdateInput();
                txtSend.Text = "";
            }
        }

        private void UpdateInput()
        {
            rtbInput.Rtf = (chat.ChatMemory[chat.CurrentUser.ID] == null) ? "" :
                chat.ChatMemory.ToRTF(chat.ChatMemory[chat.CurrentUser.ID], chat.Me);
        }

        private void userList1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                foreach (IClient c in userList1.DataSource)
                    if ((string)userList1.Rows[e.RowIndex].Cells[1].Value == c.Name)
                    {
                        chat.CurrentUser = c;
                        break;
                    }
                panel1.Controls.Clear();
                switch (state)
                {
                    case State.None:
                        ShowInput(true);
                        if (chat.Me.Notification.Contains(chat.CurrentUser.ID))
                        {
                            chat.Me.RemoveNotification(chat.CurrentUser.ID);
                            userList1.UpdateItem(chat.CurrentUser);
                            var arr = chat.Server.Message(chat.CurrentUser, chat.Me, MessageType.Read);
                            foreach (MessageContainer msc in arr)
                                chat.ChatMemory.Add(chat.CurrentUser.ID, msc);

                        }
                        UpdateInput();
                        userInfo.Init();
                        panel1.Controls.Add(userInfo);
                        break;
                    case State.Request:
                        friendRequest.Init();
                        panel1.Controls.Add(friendRequest);
                        break;

                    case State.SheachingFriend:
                        friendSearch.Init();
                        panel1.Controls.Add(friendSearch);
                        break;
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtSend.Text != txtSend.DefaultValue)
                UpdateInput(txtSend.Text);
        }

        private void tbcUsers_Selected(object sender, TabControlEventArgs e)
        {
            txtSearch.Text = txtSearch.DefaultValue;
            ShowInput(false);
            switch (e.TabPageIndex)
            {
                case 0:
                    state = State.None;
                    tabPage1.Controls.Add(userList1);
                    userList1.DataSource = chat.Friends;
                    break;
                case 1:
                    state = State.SheachingFriend;
                    tabPage2.Controls.Add(userList1);
                    userList1.DataSource = chat.AllUsers;
                    break;
                case 2:
                    state = State.Request;
                    tabPage3.Controls.Add(userList1);
                    userList1.DataSource = chat.Server.FriendRequest(chat.Me);
                    break;
            }
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            if (txtSearch.Text != txtSearch.DefaultValue && txtSearch.Text.TrimEnd() != "")
            {
                switch (state)
                {
                    case State.SheachingFriend:
                        userList1.DataSource = chat.Server.SearchUser(txtSearch.Text, chat.Me);
                        break;
                    case State.None:
                        List<IClient> arr = new List<IClient>();
                        foreach (IClient friends in chat.Friends)
                            if (friends.Name.Contains(txtSearch.Text))
                                arr.Add(friends);
                        userList1.DataSource = arr;
                        break;
                }
            }
            else if (txtSearch.Text == "")
            {
                switch (state)
                {
                    case State.SheachingFriend:
                        userList1.DataSource = chat.Server.SearchUser(txtSearch.Text, chat.Me);
                        break;
                    case State.Request:
                        userList1.DataSource = chat.Server.FriendRequest(chat.Me);
                        break;
                    case State.None:
                        userList1.DataSource = chat.Friends;
                        break;
                }

            }
        }

        private void tsmHistory_Click(object sender, EventArgs e)
        {
            var arr = chat.Server.Message(chat.Me, chat.CurrentUser,
                MessageType.History, chat.ChatMemory.Rows(chat.CurrentUser.ID));
            if (arr.Count > 0)
            {
                chat.ChatMemory.Add(chat.CurrentUser.ID, arr);
                UpdateInput();
            }
        }
    }
}
