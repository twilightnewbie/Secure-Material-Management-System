using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WindowsFormsApplication1
{
    public partial class frmdau : Form
    {
        // ================== KHAI BÁO BIẾN THÀNH PHẦN ==================
        private Form currentForm;
        private Timer sessionTimer;
        private DataGridView dgvCTHD;
        private TextBox txtMaHD, txtMaVT, txtSoLuong, txtDonGia;
        private string duongDanFileGoc = "";

        public frmdau()
        {
            InitializeComponent();
            InitRuntimeUI();
            ShowHome();
            StartSessionWatcher();
        }

        private void InitRuntimeUI()
        {
            lblHeaderInfo.Text = $"USER: {Database.CurrentUser} | SESSION: {Database.CurrentSessionId}";
            InitSidebar();
        }

        // ================== SIDEBAR NAVIGATION ==================
        private void InitSidebar()
        {
            panelSidebar.Controls.Clear();
            int y = 20;

            // Nhóm nghiệp vụ
            AddSideBtn("🏠 Trang chủ", (s, e) => ShowHome(), ref y);
            AddSideBtn("📑 Quản lý Hóa đơn", (s, e) => ShowInvoiceModule(), ref y);
            AddSideBtn("📊 Xem dữ liệu", (s, e) => ShowForm(new FormDuLieu()), ref y);
            

            // Nhóm bảo mật Admin
            if (Database.IsAdmin)
            {
                AddSideBtn("👤 Quản trị User", (s, e) => ShowForm(new FormUserLock()), ref y);
                AddSideBtn("💾 Hạn mức Quota", (s, e) => ShowForm(new FormQuotaManager()), ref y);
                AddSideBtn("🔐 Mã hóa DES", (s, e) => ShowForm(new FormDES()), ref y);
                AddSideBtn("🔏 Chữ ký RSA (PDF)", (s, e) => ShowForm(new FormSignPDF()), ref y);
                AddSideBtn("🔒 Mã hóa RSA & DES", (s, e) => ShowForm(new FormMain()), ref y);
                AddSideBtn("🔢 Mã hóa Cộng/Nhân", (s, e) => ShowForm(new FormCongNhanMod()), ref y);
                AddSideBtn("💻 Thiết bị đăng nhập", (s, e) => ShowForm(new FormDevices()), ref y);
            }
            AddBottomButtons();
        }

        // ================== QUẢN LÝ HÓA ĐƠN ==================
        private void ShowInvoiceModule()
        {
            currentForm?.Close();
            panelContent.Controls.Clear();

            dgvCTHD = new DataGridView { Location = new Point(20, 60), Size = new Size(950, 320), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, BackgroundColor = Color.White, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvCTHD.CellClick += (s, e) => {
                if (e.RowIndex >= 0)
                {
                    var r = dgvCTHD.Rows[e.RowIndex];
                    txtMaHD.Text = r.Cells[0].Value?.ToString(); txtMaVT.Text = r.Cells[1].Value?.ToString();
                    txtSoLuong.Text = r.Cells[2].Value?.ToString(); txtDonGia.Text = r.Cells[3].Value?.ToString();
                }
            };

            GroupBox gb = new GroupBox { Text = "Thông tin hóa đơn", Location = new Point(20, 400), Size = new Size(480, 250), Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold) };
            txtMaHD = CreateInput(gb, "Mã Hóa Đơn:", 40); txtMaVT = CreateInput(gb, "Mã Vật Tư:", 90);
            txtSoLuong = CreateInput(gb, "Số Lượng:", 140); txtDonGia = CreateInput(gb, "Đơn Giá:", 190);

            AddActionBtn("THÊM", Color.SeaGreen, 520, 415, BtnThem_Click);
            AddActionBtn("SỬA", Color.DodgerBlue, 520, 465, BtnSua_Click);
            AddActionBtn("XÓA", Color.Firebrick, 520, 515, BtnXoa_Click);
            AddActionBtn("XUẤT PDF", Color.DarkOrange, 670, 415, BtnXuatHD_Click);
            AddActionBtn("MÃ HÓA PDF", Color.DarkViolet, 670, 465, BtnMaHoaRSA_Click);
            AddActionBtn("LÀM MỚI", Color.Gray, 670, 515, (s, e) => TaiDuLieu());

            panelContent.Controls.Add(new Label { Text = "QUẢN LÝ CHI TIẾT HÓA ĐƠN", Font = new System.Drawing.Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(20, 10), AutoSize = true });
            panelContent.Controls.AddRange(new Control[] { dgvCTHD, gb });
            TaiDuLieu();
        }

        // ================== CÁC SỰ KIỆN HỆ THỐNG (FIX LỖI CS1061) ==================
        private void Frmdau_FormClosed(object sender, FormClosedEventArgs e)
        {
            sessionTimer?.Stop();
            Database.CloseConnection();
        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e) { }

        // ================== MÃ HÓA & PDF ==================
        private void BtnMaHoaRSA_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(duongDanFileGoc) || !File.Exists(duongDanFileGoc))
            {
                MessageBox.Show("⚠ Hãy nhấn XUẤT PDF trước!"); return;
            }
            try
            {
                byte[] data = File.ReadAllBytes(duongDanFileGoc);
                string encryptedPath = duongDanFileGoc.Replace(".pdf", "_Encrypted.pdf");
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
                {
                    File.WriteAllBytes(encryptedPath, data);
                }
                MessageBox.Show($"🔒 Bảo mật RSA thành công!\nLưu tại: {encryptedPath}");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi mã hóa: " + ex.Message); }
        }

        private void BtnXuatHD_Click(object sender, EventArgs e)
        {
            if (dgvCTHD.DataSource == null) return;
            SaveFileDialog sfd = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf", FileName = "hoadon.pdf" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                duongDanFileGoc = sfd.FileName;
                ExportPDF_NoPassword((DataTable)dgvCTHD.DataSource, duongDanFileGoc);
                MessageBox.Show("✅ Xuất PDF thành công!");
            }
        }

        private void ExportPDF_NoPassword(DataTable dt, string path)
        {
            BaseFont bf = BaseFont.CreateFont(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 10);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4); PdfWriter.GetInstance(doc, fs);
                doc.Open(); PdfPTable table = new PdfPTable(dt.Columns.Count) { WidthPercentage = 100 };
                foreach (DataColumn c in dt.Columns) table.AddCell(new Phrase(c.ColumnName, f));
                foreach (DataRow r in dt.Rows) foreach (var cell in r.ItemArray) table.AddCell(new Phrase(cell?.ToString(), f));
                doc.Add(table); doc.Close();
            }
        }

        // ================== HÀM TRỢ GIÚP UI & DATA ==================
        private void TaiDuLieu()
        {
            try
            {
                DataTable dt = new DataTable();
                new OracleDataAdapter("SELECT MaHD, MaVatTu, SoLuong, DonGia FROM CT_HOADON", Database.Conn).Fill(dt);
                dgvCTHD.DataSource = dt;
            }
            catch { }
        }

        private void RunSQL(string sql, params OracleParameter[] p)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand(sql, Database.Conn))
                {
                    if (Database.Conn.State != ConnectionState.Open) Database.Conn.Open();
                    cmd.Parameters.AddRange(p); cmd.ExecuteNonQuery(); TaiDuLieu();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnThem_Click(object sender, EventArgs e) => RunSQL("INSERT INTO CT_HOADON VALUES (:h,:v,:s,:d)", new OracleParameter("h", txtMaHD.Text), new OracleParameter("v", txtMaVT.Text), new OracleParameter("s", txtSoLuong.Text), new OracleParameter("d", txtDonGia.Text));
        private void BtnSua_Click(object sender, EventArgs e) => RunSQL("UPDATE CT_HOADON SET SoLuong=:s, DonGia=:d WHERE MaHD=:h AND MaVatTu=:v", new OracleParameter("s", txtSoLuong.Text), new OracleParameter("d", txtDonGia.Text), new OracleParameter("h", txtMaHD.Text), new OracleParameter("v", txtMaVT.Text));
        private void BtnXoa_Click(object sender, EventArgs e) { if (MessageBox.Show("Xác nhận xóa?", "Hỏi", MessageBoxButtons.YesNo) == DialogResult.Yes) RunSQL("DELETE FROM CT_HOADON WHERE MaHD=:h AND MaVatTu=:v", new OracleParameter("h", txtMaHD.Text), new OracleParameter("v", txtMaVT.Text)); }

        private void ShowForm(Form f) { currentForm?.Close(); panelContent.Controls.Clear(); currentForm = f; f.TopLevel = false; f.FormBorderStyle = FormBorderStyle.None; f.Dock = DockStyle.Fill; panelContent.Controls.Add(f); f.Show(); }
        private void ShowHome() { panelContent.Controls.Clear(); panelContent.Controls.Add(new Label { Text = "HỆ THỐNG QUẢN TRỊ VẬT TƯ ", Font = new System.Drawing.Font("Segoe UI", 20, FontStyle.Bold), Location = new Point(40, 40), AutoSize = true }); }
        private void AddSideBtn(string t, EventHandler a, ref int y) { Button b = new Button { Text = "   " + t, Location = new Point(0, y), Size = new Size(260, 45), FlatStyle = FlatStyle.Flat, ForeColor = Color.Gainsboro, TextAlign = ContentAlignment.MiddleLeft }; b.Click += a; panelSidebar.Controls.Add(b); y += 47; }
        private void AddActionBtn(string t, Color c, int x, int y, EventHandler a) { Button b = new Button { Text = t, Location = new Point(x, y), Size = new Size(130, 45), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold) }; b.Click += a; panelContent.Controls.Add(b); }
        private TextBox CreateInput(GroupBox gb, string l, int y) { gb.Controls.Add(new Label { Text = l, Location = new Point(20, y + 5), AutoSize = true }); TextBox t = new TextBox { Location = new Point(140, y), Width = 300 }; gb.Controls.Add(t); return t; }
        private void AddBottomButtons()
        {
            Button btnExit = new Button { Text = "🚪 THOÁT", Dock = DockStyle.Bottom, Height = 55, BackColor = Color.Firebrick, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnExit.Click += (s, e) => Application.Exit();
            if (Database.IsAdmin)
            {
                Button btnReg = new Button { Text = "📝 ĐĂNG KÝ USER", Dock = DockStyle.Bottom, Height = 55, BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnReg.Click += (s, e) => new Form2().ShowDialog(); panelSidebar.Controls.Add(btnReg);
            }
            panelSidebar.Controls.Add(btnExit);
        }

        private void StartSessionWatcher() { sessionTimer = new Timer { Interval = 3000 }; sessionTimer.Tick += (s, e) => { if (!IsSessionAlive()) ForceLogout(); }; sessionTimer.Start(); }
        private bool IsSessionAlive() { try { using (var cmd = Database.Conn.CreateCommand()) { cmd.CommandText = "SELECT 1 FROM dual"; cmd.ExecuteScalar(); return true; } } catch { return false; } }
        private void ForceLogout() { sessionTimer.Stop(); MessageBox.Show("Session bị ngắt!"); Database.CloseConnection(); Hide(); new Form1().Show(); Close(); }
    }
}