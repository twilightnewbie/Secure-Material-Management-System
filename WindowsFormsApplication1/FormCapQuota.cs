using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;
using static WindowsFormsApplication1.Form1;

namespace WindowsFormsApplication1
{
    public partial class FormCapQuota : Form
    {
        public FormCapQuota()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnCap_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim().ToUpper();
            string quota = txtQuota.Text.Trim();
            string tablespace = txtTablespace.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(quota))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            try
            {
                using (var conn = Database.Conn)
                {
                    string sql = $"ALTER USER {username} QUOTA {quota}M ON {tablespace}";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show($"✅ Cấp quota {quota}M cho user {username} thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message);
            }
        }

        private void btnUnlimited_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim().ToUpper();
            string tablespace = txtTablespace.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên user!");
                return;
            }

            try
            {
                using (var conn = Database.Conn)
                {
                    string sql = $"ALTER USER {username} QUOTA UNLIMITED ON {tablespace}";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show($"♾️ Đặt quota UNLIMITED cho user {username} thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message);
            }
        }
    }
}
