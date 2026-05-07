using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Security.Cryptography;
using static WindowsFormsApplication1.Form1;
using Font = System.Drawing.Font;

namespace WindowsFormsApplication1
{
    public partial class FormHoaDon : Form
    {
        private DataGridView dgvCTHD;
        private TextBox txtMaHD, txtMaVT, txtSoLuong, txtDonGia;
        private OracleConnection conn;
        private string duongDanFileGoc = "";

        public FormHoaDon()
        {
            InitializeComponent();
            conn = Database.Conn;
            InitUI();
            TaiDuLieu();
        }

        // =================================================
        // UI
        // =================================================
        private void InitUI()
        {
            this.BackColor = Color.WhiteSmoke;

            Label title = new Label
            {
                Text = "QUẢN LÝ CHI TIẾT HÓA ĐƠN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 10)
            };
            this.Controls.Add(title);

            dgvCTHD = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(950, 320),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvCTHD.CellClick += DgvCTHD_CellClick;
            this.Controls.Add(dgvCTHD);

            GroupBox gb = new GroupBox
            {
                Text = "Thông tin hóa đơn",
                Location = new Point(20, 400),
                Size = new Size(480, 250),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            this.Controls.Add(gb);

            txtMaHD = CreateInput(gb, "Mã Hóa Đơn:", 40);
            txtMaVT = CreateInput(gb, "Mã Vật Tư:", 90);
            txtSoLuong = CreateInput(gb, "Số Lượng:", 140);
            txtDonGia = CreateInput(gb, "Đơn Giá:", 190);

            AddBtn("THÊM", Color.SeaGreen, 520, 415, BtnThem_Click);
            AddBtn("SỬA", Color.DodgerBlue, 520, 465, BtnSua_Click);
            AddBtn("XÓA", Color.Firebrick, 520, 515, BtnXoa_Click);
            AddBtn("XUẤT PDF", Color.DarkOrange, 670, 415, BtnXuatPDF_Click);
            AddBtn("MÃ HÓA PDF", Color.DarkViolet, 670, 465, BtnMaHoaPDF_Click);
            AddBtn("LÀM MỚI", Color.Gray, 670, 515, (s, e) => TaiDuLieu());
        }

        private TextBox CreateInput(GroupBox gb, string label, int y)
        {
            gb.Controls.Add(new Label { Text = label, Location = new Point(20, y + 5) });
            TextBox t = new TextBox { Location = new Point(140, y), Width = 300 };
            gb.Controls.Add(t);
            return t;
        }

        private void AddBtn(string text, Color c, int x, int y, EventHandler e)
        {
            Button b = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(130, 45),
                BackColor = c,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            b.Click += e;
            this.Controls.Add(b);
        }

        // =================================================
        // DATA
        // =================================================
        private void TaiDuLieu()
        {
            OracleDataAdapter da = new OracleDataAdapter(
                "SELECT MaHD, MaVatTu, SoLuong, DonGia FROM CT_HOADON",
                conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvCTHD.DataSource = dt;
        }

        private void DgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var r = dgvCTHD.Rows[e.RowIndex];
            txtMaHD.Text = r.Cells[0].Value.ToString();
            txtMaVT.Text = r.Cells[1].Value.ToString();
            txtSoLuong.Text = r.Cells[2].Value.ToString();
            txtDonGia.Text = r.Cells[3].Value.ToString();
        }

        private void RunSQL(string sql, params OracleParameter[] p)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                cmd.Parameters.AddRange(p);
                cmd.ExecuteNonQuery();
                TaiDuLieu();
            }
        }

        private void BtnThem_Click(object s, EventArgs e)
        {
            RunSQL("INSERT INTO CT_HOADON VALUES (:hd,:vt,:sl,:dg)",
                new OracleParameter("hd", txtMaHD.Text),
                new OracleParameter("vt", txtMaVT.Text),
                new OracleParameter("sl", txtSoLuong.Text),
                new OracleParameter("dg", txtDonGia.Text));
        }

        private void BtnSua_Click(object s, EventArgs e)
        {
            RunSQL("UPDATE CT_HOADON SET SoLuong=:sl, DonGia=:dg WHERE MaHD=:hd AND MaVatTu=:vt",
                new OracleParameter("sl", txtSoLuong.Text),
                new OracleParameter("dg", txtDonGia.Text),
                new OracleParameter("hd", txtMaHD.Text),
                new OracleParameter("vt", txtMaVT.Text));
        }

        private void BtnXoa_Click(object s, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa?", "Hỏi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                RunSQL("DELETE FROM CT_HOADON WHERE MaHD=:hd AND MaVatTu=:vt",
                    new OracleParameter("hd", txtMaHD.Text),
                    new OracleParameter("vt", txtMaVT.Text));
        }

        private void BtnXuatPDF_Click(object s, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "PDF|*.pdf", FileName = "hoadon.pdf" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            duongDanFileGoc = sfd.FileName;
            ExportPDF((DataTable)dgvCTHD.DataSource, duongDanFileGoc);
            MessageBox.Show("Xuất PDF thành công");
        }

        private void ExportPDF(DataTable dt, string path)
        {
            BaseFont bf = BaseFont.CreateFont(
                Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\\arial.ttf",
                BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 11);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                PdfPTable table = new PdfPTable(dt.Columns.Count);
                foreach (DataColumn c in dt.Columns)
                    table.AddCell(new Phrase(c.ColumnName, f));

                foreach (DataRow r in dt.Rows)
                    foreach (var item in r.ItemArray)
                        table.AddCell(new Phrase(item.ToString(), f));

                doc.Add(table);
                doc.Close();
            }
        }

        private void BtnMaHoaPDF_Click(object s, EventArgs e)
        {
            if (!File.Exists(duongDanFileGoc))
            {
                MessageBox.Show("Hãy xuất PDF trước");
                return;
            }
            byte[] data = File.ReadAllBytes(duongDanFileGoc);
            File.WriteAllBytes(duongDanFileGoc.Replace(".pdf", "_Encrypted.pdf"), data);
            MessageBox.Show("Mã hóa PDF (demo) thành công");
        }
    }
}
