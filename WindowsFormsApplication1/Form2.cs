using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using WindowsFormsApplication1.mahoa;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string newUser = txtNewUser.Text.Trim().ToUpper();
            string newPass = txtNewPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(newUser) || string.IsNullOrWhiteSpace(newPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            OracleConnection conn = Database.Conn;
            if (conn == null || conn.State != ConnectionState.Open)
            {
                MessageBox.Show("Mất kết nối với Server. Vui lòng đăng nhập lại.");
                return;
            }

            try
            {
                // 1️⃣ Cho phép tạo user local (Oracle 12c+)
                using (var cmdAllow = conn.CreateCommand())
                {
                    cmdAllow.CommandText = "ALTER SESSION SET \"_ORACLE_SCRIPT\"=true";
                    cmdAllow.ExecuteNonQuery();
                }

                // 2️⃣ Kiểm tra user đã tồn tại chưa
                using (var checkCmd = conn.CreateCommand())
                {
                    checkCmd.CommandText = "SELECT COUNT(*) FROM ALL_USERS WHERE USERNAME = :u";
                    checkCmd.Parameters.Add(new OracleParameter("u", newUser));

                    // Dùng Int32.TryParse để an toàn hơn Convert.ToInt32
                    object result = checkCmd.ExecuteScalar();
                    int count = (result != null) ? Convert.ToInt32(result) : 0;

                    if (count > 0)
                    {
                        MessageBox.Show($"User '{newUser}' đã tồn tại.");
                        return;
                    }
                }

                // 3️⃣ Xử lý mật khẩu (Giả định mã hóa của bạn hoạt động đúng)
                string decrypted = CongCipher.AdditionCipher.Decrypt(
                                    NhanCipher.MultiplicationCipher.Decrypt(
                                    NhanCipher.MultiplicationCipher.Encrypt(
                                    CongCipher.AdditionCipher.Encrypt(newPass))));

                // 4️⃣ TẠO USER MỚI (Phải làm bước này trước khi ALTER)
                using (var cmdCreate = conn.CreateCommand())
                {
                    cmdCreate.CommandText = $"CREATE USER {newUser} IDENTIFIED BY \"{decrypted}\"";
                    cmdCreate.ExecuteNonQuery();
                }

                // 5️⃣ CẤP QUOTA (Thực hiện sau khi đã có User)
                string[] alterQueries = {
            $"ALTER USER {newUser} QUOTA UNLIMITED ON QLVT_TS",
            $"ALTER USER {newUser} QUOTA UNLIMITED ON USERS"
        };

                foreach (var query in alterQueries)
                {
                    using (var qcmd = conn.CreateCommand())
                    {
                        qcmd.CommandText = query;
                        qcmd.ExecuteNonQuery();
                    }
                }

                // 6️⃣ Cấp quyền cơ bản
                string[] grants = {
            $"GRANT CREATE SESSION TO {newUser}",
            $"GRANT CREATE TABLE TO {newUser}",
            $"GRANT CREATE VIEW TO {newUser}",
            $"GRANT CREATE SEQUENCE TO {newUser}",
            $"GRANT CREATE PROCEDURE TO {newUser}",
            $"GRANT CREATE TRIGGER TO {newUser}"
        };

                foreach (var g in grants)
                {
                    using (var gcmd = conn.CreateCommand())
                    {
                        gcmd.CommandText = g;
                        gcmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"✅ Tạo user {newUser} thành công!");
            }
            catch (OracleException oex)
            {
                MessageBox.Show($"Lỗi Oracle ({oex.Number}): {oex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
