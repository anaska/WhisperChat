using System;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class LoginForm : Form
    {
        private bool closing = true;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show(this, "You need to provide user and pass.", "Error", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Program.Client = Program.Server.Login(txtUser.Text, txtPass.Text);
                Program.CurrentState = Program.State.MainForm;
                closing = false;
                Close();
            }catch(Exception ex)
            {
                MessageBox.Show(this,ex.Message,"Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closing)
                Program.CurrentState = Program.State.Quit;
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            new AccountForm().Show(this);
        }
    }
}
