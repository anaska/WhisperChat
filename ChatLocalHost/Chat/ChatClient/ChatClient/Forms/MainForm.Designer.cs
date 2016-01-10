namespace ChatClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mnuChat = new System.Windows.Forms.MenuStrip();
            this.chatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSighOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmClose = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.tbcUsers = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblFriend = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();          
            this.userList1 = new ChatClient.Controls.UserList();         
            this.txtSearch = new ChatClient.Controls.TextBoxWithValue();
            this.rtbInput = new ChatClient.Controls.ReadOnlyRichTextBox();
            this.txtSend = new ChatClient.Controls.TextBoxWithValue();
            this.mnuChat.SuspendLayout();
            this.cmsUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tbcUsers.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userList1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuChat
            // 
            this.mnuChat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chatToolStripMenuItem});
            this.mnuChat.Location = new System.Drawing.Point(0, 0);
            this.mnuChat.Name = "mnuChat";
            this.mnuChat.Size = new System.Drawing.Size(759, 24);
            this.mnuChat.TabIndex = 0;
            this.mnuChat.Text = "Chat";
            // 
            // chatToolStripMenuItem
            // 
            this.chatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSighOut,
            this.toolStripSeparator1,
            this.tsmClose});
            this.chatToolStripMenuItem.Name = "chatToolStripMenuItem";
            this.chatToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.chatToolStripMenuItem.Text = "Chat";
            // 
            // tsmSighOut
            // 
            this.tsmSighOut.Name = "tsmSighOut";
            this.tsmSighOut.Size = new System.Drawing.Size(120, 22);
            this.tsmSighOut.Text = "Sign Out";
            this.tsmSighOut.Click += new System.EventHandler(this.tsmSighOut_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(117, 6);
            // 
            // tsmClose
            // 
            this.tsmClose.Name = "tsmClose";
            this.tsmClose.Size = new System.Drawing.Size(120, 22);
            this.tsmClose.Text = "Close";
            this.tsmClose.Click += new System.EventHandler(this.tsmClose_Click);
            // 
            // cmsUsers
            // 
            this.cmsUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmHistory});
            this.cmsUsers.Name = "cmsUsers";
            this.cmsUsers.Size = new System.Drawing.Size(153, 48);
            // 
            // tsmHistory
            // 
            this.tsmHistory.Name = "tsmHistory";
            this.tsmHistory.Size = new System.Drawing.Size(152, 22);
            this.tsmHistory.Text = "Get History";
            this.tsmHistory.Click += new System.EventHandler(this.tsmHistory_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(225, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tbcUsers);
            this.splitContainer1.Panel1.Controls.Add(this.lblFriend);
            this.splitContainer1.Panel1.Controls.Add(this.txtSearch);
            this.splitContainer1.Panel1MinSize = 160;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 400;
            this.splitContainer1.Size = new System.Drawing.Size(759, 545);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Image = global::ChatClient.Properties.Resources.add;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Friend Requets";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbcUsers
            // 
            this.tbcUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcUsers.Controls.Add(this.tabPage1);
            this.tbcUsers.Controls.Add(this.tabPage2);
            this.tbcUsers.Controls.Add(this.tabPage3);
            this.tbcUsers.Location = new System.Drawing.Point(3, 59);
            this.tbcUsers.Name = "tbcUsers";
            this.tbcUsers.SelectedIndex = 0;
            this.tbcUsers.Size = new System.Drawing.Size(232, 483);
            this.tbcUsers.TabIndex = 0;
            this.tbcUsers.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbcUsers_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.userList1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(224, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Users";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(224, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DB";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(224, 457);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Friend Request";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblFriend
            // 
            this.lblFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFriend.AutoSize = true;
            this.lblFriend.ForeColor = System.Drawing.Color.Chocolate;
            this.lblFriend.Location = new System.Drawing.Point(211, 9);
            this.lblFriend.Name = "lblFriend";
            this.lblFriend.Size = new System.Drawing.Size(13, 13);
            this.lblFriend.TabIndex = 3;
            this.lblFriend.Text = "0";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.AutoScrollMinSize = new System.Drawing.Size(200, 0);
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            this.splitContainer2.Panel1.Controls.Add(this.rtbInput);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtSend);
            this.splitContainer2.Panel2.Controls.Add(this.btnSend);
            this.splitContainer2.Size = new System.Drawing.Size(522, 545);
            this.splitContainer2.SplitterDistance = 497;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(4, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 100);
            this.panel1.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(402, 9);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(117, 26);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);            
            // 
            // userList1
            // 
            this.userList1.AllowUserToAddRows = false;
            this.userList1.AllowUserToDeleteRows = false;
            this.userList1.AllowUserToResizeColumns = false;
            this.userList1.AllowUserToResizeRows = false;
            this.userList1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.userList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userList1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.userList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userList1.ColumnHeadersVisible = false;
            this.userList1.ContextMenuStrip = this.cmsUsers;
            this.userList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userList1.Location = new System.Drawing.Point(3, 3);
            this.userList1.Name = "userList1";
            this.userList1.ReadOnly = true;
            this.userList1.RowHeadersVisible = false;
            this.userList1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userList1.Size = new System.Drawing.Size(218, 451);
            this.userList1.TabIndex = 0;
            this.userList1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userList1_CellClick_1);           
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.DefaultValue = "Search";
            this.txtSearch.IsSet = false;
            this.txtSearch.Location = new System.Drawing.Point(0, 33);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(232, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Search";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged_1);
            // 
            // rtbInput
            // 
            this.rtbInput.AcceptsTab = true;
            this.rtbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbInput.BackColor = System.Drawing.SystemColors.Control;
            this.rtbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbInput.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtbInput.Location = new System.Drawing.Point(3, 103);
            this.rtbInput.Name = "rtbInput";
            this.rtbInput.ReadOnly = true;
            this.rtbInput.Size = new System.Drawing.Size(518, 400);
            this.rtbInput.TabIndex = 0;
            this.rtbInput.Text = "";
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.DefaultValue = "Type a message here";
            this.txtSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSend.IsSet = false;
            this.txtSend.Location = new System.Drawing.Point(4, 8);
            this.txtSend.MaxLength = 255;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(392, 26);
            this.txtSend.TabIndex = 0;
            this.txtSend.Text = "Type a message here";
            this.txtSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSend_KeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 569);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mnuChat);
            this.MainMenuStrip = this.mnuChat;
            this.MinimumSize = new System.Drawing.Size(747, 602);
            this.Name = "MainForm";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mnuChat.ResumeLayout(false);
            this.mnuChat.PerformLayout();
            this.cmsUsers.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tbcUsers.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userList1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuChat;
        private System.Windows.Forms.ToolStripMenuItem chatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmSighOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnSend;
        private Controls.ReadOnlyRichTextBox rtbInput;
        private Controls.TextBoxWithValue txtSearch;
        private System.Windows.Forms.Label lblFriend;
        private Controls.UserList userList1;
        private Controls.TextBoxWithValue txtSend;
        private System.Windows.Forms.ContextMenuStrip cmsUsers;
        private System.Windows.Forms.ToolStripMenuItem tsmHistory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tbcUsers;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;        
        private System.Windows.Forms.TabPage tabPage3;  
        private System.Windows.Forms.Label label1;       
    }
}

