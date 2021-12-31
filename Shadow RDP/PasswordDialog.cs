using ShadowClassLibrary;
using System;
using System.Windows.Forms;
namespace Shadow_RDP
{
    public partial class PasswordDialog : Form
    {
        AdminCreds AdminCreds = new AdminCreds();
        public PasswordDialog()
        {
            InitializeComponent();
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserName.Text))
            {
                txtUserName.Focus();
                return;
            }
            else
            {
                if (String.IsNullOrEmpty(txtPassword.Text))
                {
                    txtPassword.Focus();
                    return;
                }
            }

            ActiveDirectory login = new ActiveDirectory(txtUserName.Text, txtPassword.Text);
            if (login.IsValid)
            {
                if (login.isMemberOf(txtUserName.Text))
                {
                    DialogResult = DialogResult.OK;
                }
                else error("You are NOT Authorized to access the station!");
            }
            else error("UserName or Password is Incorrect Please Retry.");

        }
        private void error(string text = "Please Enter Credentials")
        {
            MessageBox.Show(text, "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Clear();
            txtUserName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
