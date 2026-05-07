using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace WindowsFormsApplication1
{
    public static class Database
    {
        // Các biến tĩnh lưu trữ trạng thái hệ thống
        public static OracleConnection Conn;
        public static string CurrentUser;
        public static string CurrentSessionId;
        public static bool IsAdmin;
        public static string Host, Port, Sid;

        // 1. Hàm tạo chuỗi kết nối
        public static string BuildConnectionString(string host, string port, string service, string user, string pass)
        {
            return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))" +
                   $"(CONNECT_DATA=(SERVICE_NAME={service}))); User Id={user}; Password={pass}";
        }

        // 2. Hàm mở kết nối và khởi tạo phiên làm việc
        public static OracleConnection OpenNewConnection(string host, string port, string service, string user, string pass)
        {
            Host = host; Port = port; Sid = service; CurrentUser = user;
            string cs = BuildConnectionString(host, port, service, user, pass);
            Conn = new OracleConnection(cs);
            Conn.Open();

            // Lấy SID của phiên hiện tại
            using (var cmd = new OracleCommand("SELECT sys_context('USERENV', 'SID') FROM dual", Conn))
                CurrentSessionId = cmd.ExecuteScalar()?.ToString();

            // Gán Identifier để đồng bộ với V$SESSION
            using (var cmd = new OracleCommand($"BEGIN DBMS_SESSION.SET_IDENTIFIER('{Environment.MachineName}'); END;", Conn))
                cmd.ExecuteNonQuery();

            return Conn;
        }

        // 3. Hàm GHI LOG ĐĂNG NHẬP (Giải quyết lỗi CS0117 của bạn)
        public static void RecordLogin(string deviceName)
        {
            // Thêm LUUDATABASE. trước tên bảng
            string sql = "INSERT INTO LUUDATABASE.LOGIN_HISTORY (USERNAME, DEVICE_NAME, STATUS) VALUES (:u, :d, 'Đang hoạt động')";
            using (var cmd = new OracleCommand(sql, Conn))
            {
                cmd.Parameters.Add("u", CurrentUser);
                cmd.Parameters.Add("d", deviceName);
                cmd.ExecuteNonQuery();
            }
        }
        public static void RevokeDevice(int id)
        {
            // Cập nhật trạng thái để thiết bị không hiện ở danh sách đang hoạt động nữa
            string sql = "UPDATE USER_DEVICE SET IS_REVOKED = 'Y' WHERE ID = :id";
            using (OracleCommand cmd = new OracleCommand(sql, Conn))
            {
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                cmd.ExecuteNonQuery();
            }
        }
        public static int CountActiveSessions(string username)
        {
            string sql = "SELECT COUNT(*) FROM V$SESSION WHERE USERNAME = :u AND TYPE != 'BACKGROUND'";
            try
            {
                using (var cmd = new OracleCommand(sql, Conn))
                {
                    cmd.Parameters.Add("u", OracleDbType.Varchar2).Value = username.ToUpper();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch { return 0; }
        }

        // 5. Hàm lấy lịch sử đăng nhập để hiển thị lên Grid
        public static DataTable GetLoginHistory(string username)
        {
            string sql = @"
                SELECT h.ID, h.DEVICE_NAME, h.LOGIN_TIME, h.STATUS, s.SID, s.SERIAL#
                FROM LOGIN_HISTORY h
                LEFT JOIN V$SESSION s ON s.CLIENT_IDENTIFIER = h.DEVICE_NAME 
                WHERE h.USERNAME = :u
                ORDER BY h.LOGIN_TIME DESC";

            using (OracleCommand cmd = new OracleCommand(sql, Conn))
            {
                cmd.Parameters.Add("u", OracleDbType.Varchar2).Value = username.ToUpper();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 6. Hàm ngắt kết nối thực tế
        public static void KillUserSession(int sid, int serial)
        {
            using (OracleCommand cmd = new OracleCommand("KILL_SESSION_PROC", Conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_sid", OracleDbType.Int32).Value = sid;
                cmd.Parameters.Add("p_serial", OracleDbType.Int32).Value = serial;
                cmd.ExecuteNonQuery();
            }
        }

        // 7. Hàm đóng kết nối
        public static void CloseConnection()
        {
            if (Conn != null)
            {
                if (Conn.State == ConnectionState.Open) Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }
    }
}