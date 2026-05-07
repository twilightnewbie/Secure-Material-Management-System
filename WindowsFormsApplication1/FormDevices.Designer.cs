namespace WindowsFormsApplication1
{
    partial class FormDevices
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvDevices;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.CheckBox chkShowHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvDevices = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.chkShowHistory = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).BeginInit();
            this.SuspendLayout();

            // dgvDevices
            this.dgvDevices.AllowUserToAddRows = false;
            this.dgvDevices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDevices.Location = new System.Drawing.Point(12, 50);
            this.dgvDevices.Size = new System.Drawing.Size(760, 300);

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(12, 360);
            this.btnRefresh.Size = new System.Drawing.Size(120, 35);
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // chkShowHistory
            this.chkShowHistory.Location = new System.Drawing.Point(150, 365);
            this.chkShowHistory.Size = new System.Drawing.Size(200, 24);
            this.chkShowHistory.Text = "Xem toàn bộ lịch sử";
            this.chkShowHistory.CheckedChanged += new System.EventHandler(this.chkShowHistory_CheckedChanged);

            // btnRemove
            this.btnRemove.Location = new System.Drawing.Point(650, 360);
            this.btnRemove.Size = new System.Drawing.Size(120, 35);
            this.btnRemove.Text = " Ngắt kết nối";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);

            // FormDevices
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.chkShowHistory);
            this.Controls.Add(this.dgvDevices);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnRemove);
            this.Text = "LỊCH SỬ ĐĂNG NHẬP HỆ THỐNG";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}