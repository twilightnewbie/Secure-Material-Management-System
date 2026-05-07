using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormCongNhanMod : Form
    {
        // Khai báo các Controls với style hiện đại
        private ComboBox cboMethod;
        private NumericUpDown numA, numB, numMod;
        private TextBox txtInput, txtOutput;
        private Button btnEncrypt, btnDecrypt, btnClear, btnClose;

        public FormCongNhanMod()
        {
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.BackColor = Color.FromArgb(245, 245, 250);
            InitUI();
            UpdateControlState();
        }

        private void InitUI()
        {
            this.Text = "Mã hóa mức ứng dụng: Cộng / Nhân (mod n)";
            this.Size = new Size(800, 600);
            this.MinimumSize = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- Layout chính dùng TableLayoutPanel ---
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F)); // Khu vực cấu hình
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Khu vực nhập/xuất
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // Khu vực nút bấm

            // 1. GroupBox Cấu hình
            GroupBox grpConfig = new GroupBox { Text = "Cấu hình Thuật toán", Dock = DockStyle.Fill, ForeColor = Color.DarkSlateBlue };
            FlowLayoutPanel flowConfig = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            cboMethod = new ComboBox { Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cboMethod.Items.AddRange(new object[] { "Cộng (Caesar): C = P + b", "Nhân: C = a × P" });
            cboMethod.SelectedIndex = 0;
            cboMethod.SelectedIndexChanged += (s, e) => UpdateControlState();

            numA = CreateNumeric("a:", flowConfig);
            numB = CreateNumeric("b:", flowConfig);
            numMod = CreateNumeric("mod n:", flowConfig, 256);

            flowConfig.Controls.Add(new Label { Text = "Phương pháp:", AutoSize = true, Margin = new Padding(0, 5, 5, 0) });
            flowConfig.Controls.Add(cboMethod);
            grpConfig.Controls.Add(flowConfig);

            // 2. Khu vực Nhập liệu & Kết quả
            TableLayoutPanel textLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            textLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            textLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            GroupBox grpInput = new GroupBox { Text = "Văn bản đầu vào (Plaintext/Ciphertext)", Dock = DockStyle.Fill };
            txtInput = new TextBox { Multiline = true, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical, BorderStyle = BorderStyle.FixedSingle };
            grpInput.Controls.Add(txtInput);

            GroupBox grpOutput = new GroupBox { Text = "Kết quả", Dock = DockStyle.Fill };
            txtOutput = new TextBox { Multiline = true, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical, ReadOnly = true, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            grpOutput.Controls.Add(txtOutput);

            textLayout.Controls.Add(grpInput, 0, 0);
            textLayout.Controls.Add(grpOutput, 0, 1);

            // 3. Khu vực Nút bấm
            FlowLayoutPanel flowButtons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };

            btnClose = CreateButton("Đóng", Color.Gray, (s, e) => this.Close());
            btnClear = CreateButton("Xóa trống", Color.IndianRed, (s, e) => { txtInput.Clear(); txtOutput.Clear(); });
            btnDecrypt = CreateButton("Giải mã", Color.FromArgb(0, 122, 204), (s, e) => Process(false));
            btnEncrypt = CreateButton("Mã hóa", Color.FromArgb(34, 139, 34), (s, e) => Process(true));

            flowButtons.Controls.Add(btnClose);
            flowButtons.Controls.Add(btnClear);
            flowButtons.Controls.Add(btnDecrypt);
            flowButtons.Controls.Add(btnEncrypt);

            // Add all to main layout
            mainLayout.Controls.Add(grpConfig, 0, 0);
            mainLayout.Controls.Add(textLayout, 0, 1);
            mainLayout.Controls.Add(flowButtons, 0, 2);

            this.Controls.Add(mainLayout);
        }

        // Helper: Tạo ô nhập số
        private NumericUpDown CreateNumeric(string label, FlowLayoutPanel container, int val = 1)
        {
            Label lbl = new Label { Text = label, AutoSize = true, Margin = new Padding(10, 5, 2, 0) };
            NumericUpDown num = new NumericUpDown { Width = 70, Minimum = 0, Maximum = 65535, Value = val };
            container.Controls.Add(lbl);
            container.Controls.Add(num);
            return num;
        }

        // Helper: Tạo nút bấm
        private Button CreateButton(string text, Color backColor, EventHandler onClick)
        {
            Button btn = new Button
            {
                Text = text,
                Width = 110,
                Height = 35,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(5)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += onClick;
            return btn;
        }

        private void UpdateControlState()
        {
            bool isCong = cboMethod.SelectedIndex == 0;
            numA.Enabled = !isCong;
            numB.Enabled = isCong;
            numA.BackColor = isCong ? Color.LightGray : Color.White;
            numB.BackColor = isCong ? Color.White : Color.LightGray;
        }

        // Logic xử lý (giữ nguyên toán học nhưng tối ưu trải nghiệm)
        private void Process(bool encrypt)
        {
            if (string.IsNullOrEmpty(txtInput.Text)) return;

            int a = (int)numA.Value;
            int b = (int)numB.Value;
            int n = (int)numMod.Value;
            StringBuilder sb = new StringBuilder();

            try
            {
                if (cboMethod.SelectedIndex == 1) // Nhân
                {
                    if (!IsCoprime(a, n))
                    {
                        MessageBox.Show($"Lỗi: 'a'({a}) và 'n'({n}) phải nguyên tố cùng nhau!", "Toán học sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                foreach (char ch in txtInput.Text)
                {
                    int p = (int)ch;
                    int c;

                    if (cboMethod.SelectedIndex == 0) // CỘNG
                    {
                        c = encrypt ? (p + b) % n : (p - b + n) % n;
                    }
                    else // NHÂN
                    {
                        int aInv = encrypt ? a : ModInverse(a, n);
                        c = (aInv * p) % n;
                    }
                    sb.Append((char)c);
                }
                txtOutput.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsCoprime(int a, int b)
        {
            while (b != 0) { int t = a % b; a = b; b = t; }
            return a == 1;
        }

        private int ModInverse(int a, int n)
        {
            for (int x = 1; x < n; x++)
                if ((a * x) % n == 1) return x;
            throw new Exception("Nghịch đảo modulo không tồn tại!");
        }
    }
}