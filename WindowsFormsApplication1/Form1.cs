using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                Database.OpenNewConnection(
                    txt_host.Text,
                    txt_port.Text,
                    txt_sid.Text,
                    txt_user.Text.ToUpper(),
                    txt_pass.Text
                );

                Database.CurrentUser = txt_user.Text.ToUpper();
                Database.CurrentSessionId = Guid.NewGuid().ToString();

                Database.IsAdmin =
                    Database.CurrentUser == "SYS" ||
                    Database.CurrentUser == "LUUDATABASE";
                int sessionCount = Database.CountActiveSessions(txt_user.Text);
                Database.RecordLogin(Environment.MachineName);

                if (sessionCount > 1)
                {
                    MessageBox.Show("Tài khoản này hiện đang đăng nhập ở một nơi khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Đóng kết nối vừa mở vì không hợp lệ
                    Database.CloseConnection();
                    return;
                }
                this.Hide();
                new frmdau().ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng nhập thất bại:\n" + ex.Message);
            }
        }
    }
}
