using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using static WindowsFormsApplication1.Form1;

namespace WindowsFormsApplication1
{
    public partial class FormDuLieu : Form
    {
        private ComboBox comboBoxTables;
        private DataGridView dataGridView;
        private Button btnRefresh;
        private Label lblTitle;
        private OracleConnection conn;

        public FormDuLieu()
        {
            InitializeComponent();
            KhoiTaoGiaoDien();
            KetNoiCSDL();
            TaiDanhSachBang();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = "Xem dữ liệu các bảng trong Oracle";
            this.Width = 900;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label
            {
                Text = "🔹 DANH SÁCH DỮ LIỆU TRONG CƠ SỞ DỮ LIỆU 🔹",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.DarkBlue,
                AutoSize = true,
                Location = new System.Drawing.Point(200, 20)
            };
            this.Controls.Add(lblTitle);

            Label lblSelect = new Label
            {
                Text = "Chọn bảng:",
                Location = new System.Drawing.Point(50, 80),
                AutoSize = true
            };
            this.Controls.Add(lblSelect);

            comboBoxTables = new ComboBox
            {
                Location = new System.Drawing.Point(130, 75),
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboBoxTables.SelectedIndexChanged += ComboBoxTables_SelectedIndexChanged;
            this.Controls.Add(comboBoxTables);

            btnRefresh = new Button
            {
                Text = "🔄 Làm mới",
                Location = new System.Drawing.Point(400, 74),
                Size = new System.Drawing.Size(100, 30)
            };
            btnRefresh.Click += BtnRefresh_Click;
            this.Controls.Add(btnRefresh);

            dataGridView = new DataGridView
            {
                Location = new System.Drawing.Point(50, 120),
                Size = new System.Drawing.Size(780, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true
            };
            this.Controls.Add(dataGridView);
        }

        private void KetNoiCSDL()
        {
            conn = Database.Conn;
            if (conn == null)
            {
                MessageBox.Show("❌ Không thể kết nối đến Oracle!");
            }
        }

        private void TaiDanhSachBang()
        {
            try
            {
                if (conn == null || conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = @"
                    SELECT TABLE_NAME
                    FROM ALL_TABLES
                    WHERE OWNER = USER
                    ORDER BY TABLE_NAME";

                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataReader reader = cmd.ExecuteReader();

                comboBoxTables.Items.Clear();

                while (reader.Read())
                {
                    comboBoxTables.Items.Add(reader.GetString(0));
                }

                reader.Close();

                if (comboBoxTables.Items.Count > 0)
                    comboBoxTables.SelectedIndex = 0;
                else
                    MessageBox.Show("⚠️ User hiện tại không có bảng nào trong schema.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách bảng: " + ex.Message);
            }
        }

        private void ComboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedItem != null)
            {
                string tableName = comboBoxTables.SelectedItem.ToString();
                TaiDuLieuTuBang(tableName);
            }
        }

        private void TaiDuLieuTuBang(string tableName)
        {
            try
            {
                tableName = tableName.Trim();

                string sql = $"SELECT * FROM \"{tableName}\"";

                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            TaiDanhSachBang();
        }
    }
}
