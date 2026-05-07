using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static WindowsFormsApplication1.Form1;

namespace WindowsFormsApplication1
{
    public partial class FormUserLock : Form
    {
        public FormUserLock()
        {
            InitializeComponent();
        }

        private void FormUserLock_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadProfiles();
            LoadTables();
            LoadUserGrid();   // NEW: để hiển thị user bên trái
        }


        // ================================================================
        // LOAD DANH SÁCH USER TỪ BẢNG USER_APP
        // ================================================================
        private void LoadUsers()
        {
            OracleDataAdapter da = new OracleDataAdapter(
                "SELECT USERNAME FROM DBA_USERS ORDER BY USERNAME",
                Database.Conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            cbUsers.DataSource = dt;
            cbUsers.DisplayMember = "USERNAME";
        }
        private void LoadUserGrid()
        {
            OracleDataAdapter da = new OracleDataAdapter(
                "SELECT USERNAME, ACCOUNT_STATUS, PROFILE FROM DBA_USERS ORDER BY USERNAME",
                Database.Conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvUsers.DataSource = dt;
        }

        private void LoadProfiles()
        {
            OracleDataAdapter da = new OracleDataAdapter(
                "SELECT PROFILE FROM DBA_PROFILES GROUP BY PROFILE",
                Database.Conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            cbProfiles.DataSource = dt;
            cbProfiles.DisplayMember = "PROFILE";
        }
        private void LoadTables()
        {
            OracleDataAdapter da = new OracleDataAdapter(
                "SELECT TABLE_NAME FROM USER_TABLES ORDER BY TABLE_NAME",
                Database.Conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            cbTables.DataSource = dt;
            cbTables.DisplayMember = "TABLE_NAME";
        }
        private void LoadAllUsers()
        {
            OracleDataAdapter da = new OracleDataAdapter(
                "SELECT USERNAME FROM ALL_USERS ORDER BY USERNAME",
                Database.Conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            cbUsers.DataSource = dt;
            cbUsers.DisplayMember = "USERNAME";
        }
        private void btnGrant_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đầu vào
            if (cbUsers.SelectedItem == null || cbTables.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn User và Bảng (Table) để cấp quyền!");
                return;
            }

            string user = cbUsers.Text.Trim(); // Lấy tên User
            string table = cbTables.Text.Trim(); // Lấy tên Bảng

            // 2. Tổng hợp các quyền được chọn
            List<string> rights = new List<string>();
            if (chkSelect.Checked) rights.Add("SELECT");
            if (chkInsert.Checked) rights.Add("INSERT");
            if (chkUpdate.Checked) rights.Add("UPDATE");
            if (chkDelete.Checked) rights.Add("DELETE");

            // Nếu không chọn quyền nào thì báo lỗi
            if (rights.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn quyền nào (Select, Insert, Update, Delete)!");
                return;
            }

            // 3. Tạo câu lệnh SQL (Lưu ý: Không dùng tham số cho Tên bảng/User được)
            // Cú pháp: GRANT SELECT, INSERT ON TEN_BANG TO TEN_USER
            string strRights = string.Join(", ", rights);
            string sql = $"GRANT {strRights} ON {table} TO {user}";

            OracleConnection conn = Database.Conn;
            if (conn.State == ConnectionState.Closed) conn.Open();

            try
            {
                using (var cmd = new OracleCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"✔ Đã cấp quyền {strRights} trên bảng {table} cho user {user}!");

                // 4. Quan trọng: Tải lại danh sách quyền để hiển thị ngay
                LoadPrivileges(user);
            }
            catch (OracleException ex)
            {
                MessageBox.Show($"Lỗi Oracle ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }
        private void btnRevoke_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đầu vào
            if (cbUsers.SelectedItem == null || cbTables.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn User và Bảng để thu hồi quyền!");
                return;
            }

            string user = cbUsers.Text.Trim();
            string table = cbTables.Text.Trim();

            // 2. Tổng hợp quyền cần thu hồi
            List<string> rights = new List<string>();
            if (chkSelect.Checked) rights.Add("SELECT");
            if (chkInsert.Checked) rights.Add("INSERT");
            if (chkUpdate.Checked) rights.Add("UPDATE");
            if (chkDelete.Checked) rights.Add("DELETE");

            if (rights.Count == 0)
            {
                MessageBox.Show("Hãy chọn ít nhất một quyền để thu hồi!");
                return;
            }

            // 3. Tạo câu lệnh SQL
            // Cú pháp: REVOKE SELECT, INSERT ON TEN_BANG FROM TEN_USER
            string strRights = string.Join(", ", rights);
            string sql = $"REVOKE {strRights} ON {table} FROM {user}";

            OracleConnection conn = Database.Conn;
            if (conn.State == ConnectionState.Closed) conn.Open();

            try
            {
                using (var cmd = new OracleCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"✔ Đã thu hồi quyền {strRights} của user {user} trên bảng {table}!");

                // 4. Tải lại danh sách
                LoadPrivileges(user);
            }
            catch (OracleException ex)
            {
                // Lỗi phổ biến: ORA-01927 (Thu hồi quyền mà user không có)
                if (ex.Number == 1927)
                    MessageBox.Show("Lỗi: User này không có quyền đó để mà thu hồi!");
                else
                    MessageBox.Show($"Lỗi Oracle ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void LoadPrivileges(string user)
        {
            // DBA_TAB_PRIVS chứa thông tin quyền trên các bảng
            // GRANTEE: Người được cấp quyền
            // TABLE_NAME: Tên bảng
            // PRIVILEGE: Tên quyền (SELECT, INSERT...)
            string sql = @"SELECT TABLE_NAME, PRIVILEGE, GRANTABLE 
                   FROM DBA_TAB_PRIVS 
                   WHERE GRANTEE = :u 
                   ORDER BY TABLE_NAME";

            OracleConnection conn = Database.Conn;
            if (conn.State == ConnectionState.Closed) conn.Open();

            try
            {
                OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                da.SelectCommand.Parameters.Add("u", OracleDbType.Varchar2).Value = user.ToUpper(); // Oracle lưu user chữ hoa

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPrivileges.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách quyền: " + ex.Message);
            }
        }

        private void btnApplyProfile_Click(object sender, EventArgs e)
        {
            string user = cbUsers.Text.Trim().ToUpper();
            string profile = cbProfiles.Text.Trim().ToUpper();

            string sql = $"ALTER USER {user} PROFILE {profile}";

            try
            {
                OracleCommand cmd = new OracleCommand(sql, Database.Conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show($"✔ Đã gán profile {profile} cho user {user}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gán profile: " + ex.Message);
            }
        }


        private string GetSelectedUser()
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn user.");
                return null;
            }

            return dgvUsers.CurrentRow.Cells["USERNAME"].Value.ToString();
        }

        // ================================================================
        // TẢI LẠI DANH SÁCH
        // ================================================================
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadUsers();
            LoadUserGrid();
            LoadProfiles();
            LoadTables();
        }

        // ================================================================
        // THÊM USER MỚI
        // ================================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string newUser = Microsoft.VisualBasic.Interaction.InputBox("Nhập username mới:", "Thêm User", "");
            if (string.IsNullOrWhiteSpace(newUser)) return;

            string newPass = Microsoft.VisualBasic.Interaction.InputBox("Nhập mật khẩu:", "Tạo mật khẩu", "");
            if (string.IsNullOrWhiteSpace(newPass)) return;

            string passHash = AppAuth.Sha256Hex(newPass);

            OracleConnection conn = Database.Conn;
            if (conn.State == ConnectionState.Closed) conn.Open();

            OracleTransaction trans = conn.BeginTransaction(); // Dùng Transaction để đảm bảo tính toàn vẹn

            try
            {
                
                string sqlCreateUser = $"CREATE USER {newUser} IDENTIFIED BY \"{newPass}\"";
                using (var cmd = new OracleCommand(sqlCreateUser, conn))
                {
                    cmd.Transaction = trans; // DDL trong Oracle tự commit, nhưng gán vào để quản lý flow
                    cmd.ExecuteNonQuery();
                }

                // BƯỚC 2: Cấp quyền cơ bản để User này đăng nhập được (CONNECT)
                string sqlGrantConnect = $"GRANT CREATE SESSION TO {newUser}";
                using (var cmd = new OracleCommand(sqlGrantConnect, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // BƯỚC 3: Lưu vào bảng USER_APP
                using (var cmd = new OracleCommand(
                    "INSERT INTO USER_APP (USERNAME, PASSWORD_HASH, IS_LOCKED) VALUES (:u, :p, 0)", conn))
                {
                    cmd.BindByName = true; 
                    cmd.Parameters.Add("u", newUser);
                    cmd.Parameters.Add("p", passHash);
                    cmd.ExecuteNonQuery();
                }

                trans.Commit(); // Xác nhận giao dịch
                MessageBox.Show("✔ User đã được tạo thành công trên Oracle và App!");
                LoadUsers();
            }
            catch (Exception ex)
            {
                
                trans.Rollback();
                MessageBox.Show("Lỗi tạo user:\n" + ex.Message);
            }
        }

        // ================================================================
        // KHÓA USER
        // ================================================================
        private void btnLock_Click(object sender, EventArgs e)
        {
            string user = GetSelectedUser();
            if (user == null) return;

            try
            {
                // 1. Khóa tài khoản Oracle
                string sqlOracle = $"ALTER USER {user} ACCOUNT LOCK";
                OracleCommand cmdOracle = new OracleCommand(sqlOracle, Database.Conn);
                cmdOracle.ExecuteNonQuery();

                // 2. Cập nhật trạng thái trong bảng USER_APP
                using (var cmd = new OracleCommand(
                    "UPDATE USER_APP SET IS_LOCKED = 1 WHERE USERNAME = :u",
                    Database.Conn))
                {
                    cmd.Parameters.Add("u", user);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"🔒 Đã khóa user: {user}");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khóa user: " + ex.Message);
            }
        }

        // ================================================================
        // MỞ KHÓA USER
        // ================================================================
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            string user = GetSelectedUser();
            if (user == null) return;

            OracleConnection conn = Database.Conn;
            if (conn.State == ConnectionState.Closed) conn.Open();

            try
            {
                // 1. Mở khóa trên Oracle
                string sqlUnlockOracle = $"ALTER USER {user} ACCOUNT UNLOCK";
                using (var cmd = new OracleCommand(sqlUnlockOracle, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2. Cập nhật bảng USER_APP
                string sqlUpdateApp = "UPDATE USER_APP SET IS_LOCKED = 0 WHERE USERNAME = :u";
                using (var cmd = new OracleCommand(sqlUpdateApp, conn))
                {
                    cmd.Parameters.Add("u", OracleDbType.Varchar2).Value = user;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"🔓 Đã mở khóa User '{user}'!");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở khóa: " + ex.Message);
            }
        }

        // ================================================================
        // XÓA USER
        // ================================================================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string user = GetSelectedUser();
            if (user == null) return;

            if (MessageBox.Show($"CẢNH BÁO: Bạn có chắc chắn muốn xóa vĩnh viễn user '{user}' khỏi cơ sở dữ liệu?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                return;

            OracleConnection conn = Database.Conn;
            if (conn.State == ConnectionState.Closed) conn.Open();

            try
            {
                // 1. Xóa user trong Oracle (CASCADE để xóa luôn các bảng/schema của user đó nếu có)
                string sqlDropOracle = $"DROP USER {user} CASCADE";
                using (var cmd = new OracleCommand(sqlDropOracle, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2. Xóa trong bảng USER_APP
                string sqlDeleteApp = "DELETE FROM USER_APP WHERE USERNAME = :u";
                using (var cmd = new OracleCommand(sqlDeleteApp, conn))
                {
                    cmd.Parameters.Add("u", OracleDbType.Varchar2).Value = user;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"🗑 Đã xóa hoàn toàn User '{user}'");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa user (Có thể user đang connect hoặc thiếu quyền):\n" + ex.Message);
            }
        }

        private void chkDelete_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
