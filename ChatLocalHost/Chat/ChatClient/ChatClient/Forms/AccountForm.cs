using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class AccountForm : Form
    {
        public AccountForm()
        {
            InitializeComponent();
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "" && txtPass.Text != "" && txtPass.Text == txtPassCon.Text)
            {
                if (Program.Server.Register(txtUser.Text, txtPass.Text))
                {
                    MessageBox.Show(this, "User created.", "Info", MessageBoxButtons.OK);
                    Close();
                }
                else MessageBox.Show(this, "User allready exist.Please pick another name.", "Info", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show(this, "Please fill all fills.And password confirmation need to be the same as password.", "Info", MessageBoxButtons.OK);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
