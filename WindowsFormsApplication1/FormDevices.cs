using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormDevices : Form
    {
        public FormDevices()
        {
            InitializeComponent();
        }

        private void FormDevices_Load(object sender, EventArgs e)
        {
            LoadDevices();
            btnRemove.Visible = Database.IsAdmin; // Chỉ Admin mới thấy nút Hủy
        }

        private void LoadDevices()
        {
            try
            {
                DataTable dt = Database.GetLoginHistory(Database.CurrentUser);
                dgvDevices.DataSource = dt;

                // Ẩn các cột kỹ thuật
                if (dgvDevices.Columns.Contains("SID")) dgvDevices.Columns["SID"].Visible = false;
                if (dgvDevices.Columns.Contains("SERIAL#")) dgvDevices.Columns["SERIAL#"].Visible = false;

                foreach (DataGridViewRow row in dgvDevices.Rows)
                {
                    // Kiểm tra Online dựa trên SID
                    if (row.Cells["SID"].Value != DBNull.Value && row.Cells["SID"].Value != null)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hiển thị: " + ex.Message); }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng có chọn dòng nào không
            if (dgvDevices.CurrentRow == null) return;

            // 2. Kiểm tra quyền Admin trước khi thực hiện
            if (!Database.IsAdmin)
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                // 3. Lấy giá trị thô từ các ô để tránh lỗi định dạng chuỗi
                var sidVal = dgvDevices.CurrentRow.Cells["SID"].Value;
                var serialVal = dgvDevices.CurrentRow.Cells["SERIAL#"].Value;
                int deviceId = Convert.ToInt32(dgvDevices.CurrentRow.Cells["ID"].Value); // ID thường luôn có giá trị

                // 4. Hỏi xác nhận người dùng
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn ngắt kết nối và gỡ thiết bị này?",
                                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) return;

                // 5. XỬ LÝ NGẮT SESSION (Chỉ thực hiện nếu thiết bị đang Online - có SID)
                if (sidVal != null && sidVal != DBNull.Value && !string.IsNullOrEmpty(sidVal.ToString()))
                {
                    try
                    {
                        int sid = Convert.ToInt32(sidVal);
                        int serial = Convert.ToInt32(serialVal);

                        // Gọi hàm ngắt kết nối thực tế trên Oracle
                        Database.KillUserSession(sid, serial);
                    }
                    catch (Exception ex)
                    {
                        // Nếu lỗi do thiếu quyền ORA-01031 hoặc session đã tự đóng, chỉ thông báo nhẹ
                        MessageBox.Show("Cảnh báo ngắt Session: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Nếu không có SID, thiết bị đang Offline, ta chỉ cần cập nhật DB
                    Console.WriteLine("Thiết bị đang ngoại tuyến, bỏ qua bước Kill Session.");
                }

                // 6. CẬP NHẬT TRẠNG THÁI TRONG DATABASE (Đánh dấu Revoked hoặc xóa log)
                Database.RevokeDevice(deviceId);

                // 7. Hoàn tất và tải lại dữ liệu
                MessageBox.Show("Đã gỡ thiết bị thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDevices();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện khi tích chọn Xem lịch sử
        private void chkShowHistory_CheckedChanged(object sender, EventArgs e)
        {
            LoadDevices();
        }

 

        private void btnRefresh_Click(object sender, EventArgs e) => LoadDevices();
        
    }
}