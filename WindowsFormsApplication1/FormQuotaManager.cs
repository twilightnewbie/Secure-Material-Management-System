using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;
using static WindowsFormsApplication1.Form1;

namespace WindowsFormsApplication1
{
    public partial class FormQuotaManager : Form
    {
        public FormQuotaManager()
        {
            InitializeComponent();
        }

        private void FormQuotaManager_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadTablespaces();
        }

        private void LoadUsers()
        {
            OracleCommand cmd = new OracleCommand(
                "SELECT USERNAME FROM ALL_USERS ORDER BY USERNAME",
                Database.Conn);    

            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) cbUsers.Items.Add(rd.GetString(0));
        }

        private void LoadTablespaces()
        {
            OracleCommand cmd = new OracleCommand(
                "SELECT TABLESPACE_NAME FROM DBA_TABLESPACES",
                Database.Conn);

            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) cbTablespaces.Items.Add(rd.GetString(0));
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (cbUsers.SelectedItem == null ||
                cbTablespaces.SelectedItem == null ||
                cbQuota.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đủ thông tin!");
                return;
            }

            string user = cbUsers.SelectedItem.ToString();
            string ts = cbTablespaces.SelectedItem.ToString();
            string quota = cbQuota.SelectedItem.ToString();

            string sql = $"ALTER USER {user} QUOTA {quota} ON {ts}";

            OracleCommand cmd = new OracleCommand(sql, Database.Conn);
            cmd.ExecuteNonQuery();

            MessageBox.Show("✔ Cấp quota thành công!");

            LoadUserQuota(user);
        }

        private void LoadUserQuota(string username)
        {
            OracleDataAdapter da = new OracleDataAdapter(
                "SELECT TABLESPACE_NAME, BYTES/1024/1024 AS MB " +
                "FROM USER_TS_QUOTAS WHERE USERNAME = :u",
                Database.Conn);

            da.SelectCommand.Parameters.Add("u", username);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvQuota.DataSource = dt;
        }
    }
}
