using Chat;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ChatClient.Controls
{
    public class UserList : DataGridView
    {
        private DataGridViewColumn status;
        private DataGridViewColumn user;
        private DataGridViewColumn notification;
        private List<IClient> dataSource;

        public UserList()
        {
            InitializeComponents();

        }



        public new List<IClient> DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                AddItem(dataSource);
            }
        }


        private void AddItem(List<IClient> arr)
        {
            Rows.Clear();
            if (arr != null)
                foreach (IClient c in arr)
                    AddItem(c);
        }

        public void UpdateItem(IClient client)
        {
            for (int i = 0; i < Rows.Count; i++)
                if (Rows[i].Cells[1].Value.ToString().TrimEnd() == client.Name.TrimEnd())
                {
                    Rows[i].Cells[0].Value = client.Connected ? Properties.Resources.on : Properties.Resources.off;
                    Rows[i].Cells[2].Value = Program.Client.Notification[client.ID] != null ? Properties.Resources.notification : null;
                    return;
                }
        }

        private delegate void AddItemDelegate(IClient client);

        public void AddItem(IClient client)
        {
            if (InvokeRequired)
            {
                AddItemDelegate d = new AddItemDelegate(AddItem);
                BeginInvoke(d, client);
            }
            else
            {
                if (!dataSource.Contains(client))
                    dataSource.Add(client);
                DataGridViewRow r = new DataGridViewRow();
                Rows.Add(client.Connected ? Properties.Resources.on : Properties.Resources.off
                    , client.Name,
                    Program.Client.Notification[client.ID] != null ? Properties.Resources.notification : null);                
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewColumnCollection Columns
        {
            get { return base.Columns; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(false)]
        public new bool AutoGenerateColumns
        {
            get { return false; }
        }


        private void InitializeComponents()
        {
            status = new DataGridViewColumn();
            status.CellTemplate = new DataGridViewImageCell();
            status.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            status.Width = 16;
            status.MinimumWidth = 16;
            status.FillWeight = 10F;
            status.ReadOnly = true;


            user = new DataGridViewColumn();
            user.CellTemplate = new DataGridViewTextBoxCell();
            user.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            user.Width = 60;
            user.MinimumWidth = 40;
            user.FillWeight = 85F;
            user.ReadOnly = true;

            notification = new DataGridViewColumn();
            notification.CellTemplate = new DataGridViewImageCell();
            notification.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            notification.DefaultCellStyle.NullValue = null;
            notification.Width = 8;
            notification.MinimumWidth = 8;
            notification.FillWeight = 5F;
            notification.ReadOnly = true;

            Columns.Add(status);
            Columns.Add(user);
            Columns.Add(notification);

            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;
            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReadOnly = true;
            RowHeadersVisible = false;
            ColumnHeadersVisible = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TabIndex = 0;
            BackgroundColor = SystemColors.Control;

        }
    }
}
