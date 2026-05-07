namespace WindowsFormsApplication1
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.labelFile = new System.Windows.Forms.Label();
            this.labelKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.groupDES = new System.Windows.Forms.GroupBox();
            this.btnEncryptDES = new System.Windows.Forms.Button();
            this.btnDecryptDES = new System.Windows.Forms.Button();
            this.groupRSA = new System.Windows.Forms.GroupBox();
            this.btnGenRSA = new System.Windows.Forms.Button();
            this.btnEncryptRSA = new System.Windows.Forms.Button();
            this.btnDecryptRSA = new System.Windows.Forms.Button();
            this.labelLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupDES.SuspendLayout();
            this.groupRSA.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(150, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 40);
            this.lblTitle.Text = "🔐 MÃ HÓA – GIẢI MÃ RSA & DES 🔐";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelFile.Location = new System.Drawing.Point(80, 80);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(110, 20);
            this.labelFile.Text = "📂 Chọn tập tin:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(200, 80);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(350, 23);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowse.Location = new System.Drawing.Point(570, 78);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(90, 27);
            this.btnBrowse.Text = "Chọn file...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelKey.Location = new System.Drawing.Point(80, 125);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(185, 20);
            this.labelKey.Text = "🔑 Nhập khóa mã hóa DES:";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(280, 125);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(380, 23);
            // 
            // groupDES
            // 
            this.groupDES.Controls.Add(this.btnEncryptDES);
            this.groupDES.Controls.Add(this.btnDecryptDES);
            this.groupDES.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupDES.Location = new System.Drawing.Point(100, 170);
            this.groupDES.Name = "groupDES";
            this.groupDES.Size = new System.Drawing.Size(560, 60);
            this.groupDES.Text = "🧩 Thuật toán DES";
            // 
            // btnEncryptDES
            // 
            this.btnEncryptDES.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEncryptDES.Location = new System.Drawing.Point(330, 22);
            this.btnEncryptDES.Name = "btnEncryptDES";
            this.btnEncryptDES.Size = new System.Drawing.Size(100, 28);
            this.btnEncryptDES.Text = "Mã hóa DES";
            this.btnEncryptDES.UseVisualStyleBackColor = true;
            this.btnEncryptDES.Click += new System.EventHandler(this.btnEncryptDES_Click);
            // 
            // btnDecryptDES
            // 
            this.btnDecryptDES.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDecryptDES.Location = new System.Drawing.Point(120, 22);
            this.btnDecryptDES.Name = "btnDecryptDES";
            this.btnDecryptDES.Size = new System.Drawing.Size(100, 28);
            this.btnDecryptDES.Text = "Giải mã DES";
            this.btnDecryptDES.UseVisualStyleBackColor = true;
            this.btnDecryptDES.Click += new System.EventHandler(this.btnDecryptDES_Click);
            // 
            // groupRSA
            // 
            this.groupRSA.Controls.Add(this.btnGenRSA);
            this.groupRSA.Controls.Add(this.btnEncryptRSA);
            this.groupRSA.Controls.Add(this.btnDecryptRSA);
            this.groupRSA.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupRSA.Location = new System.Drawing.Point(100, 245);
            this.groupRSA.Name = "groupRSA";
            this.groupRSA.Size = new System.Drawing.Size(560, 70);
            this.groupRSA.Text = "🔒 Thuật toán RSA";
            // 
            // btnGenRSA
            // 
            this.btnGenRSA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnGenRSA.Location = new System.Drawing.Point(50, 28);
            this.btnGenRSA.Name = "btnGenRSA";
            this.btnGenRSA.Size = new System.Drawing.Size(120, 28);
            this.btnGenRSA.Text = "Tạo khóa RSA";
            this.btnGenRSA.UseVisualStyleBackColor = true;
            this.btnGenRSA.Click += new System.EventHandler(this.btnGenRSA_Click);
            // 
            // btnEncryptRSA
            // 
            this.btnEncryptRSA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEncryptRSA.Location = new System.Drawing.Point(230, 28);
            this.btnEncryptRSA.Name = "btnEncryptRSA";
            this.btnEncryptRSA.Size = new System.Drawing.Size(120, 28);
            this.btnEncryptRSA.Text = "Mã hóa RSA";
            this.btnEncryptRSA.UseVisualStyleBackColor = true;
            this.btnEncryptRSA.Click += new System.EventHandler(this.btnEncryptRSA_Click);
            // 
            // btnDecryptRSA
            // 
            this.btnDecryptRSA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDecryptRSA.Location = new System.Drawing.Point(410, 28);
            this.btnDecryptRSA.Name = "btnDecryptRSA";
            this.btnDecryptRSA.Size = new System.Drawing.Size(120, 28);
            this.btnDecryptRSA.Text = "Giải mã RSA";
            this.btnDecryptRSA.UseVisualStyleBackColor = true;
            this.btnDecryptRSA.Click += new System.EventHandler(this.btnDecryptRSA_Click);
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelLog.Location = new System.Drawing.Point(80, 335);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(150, 20);
            this.labelLog.Text = "📋 Kết quả thực hiện:";
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtLog.Location = new System.Drawing.Point(100, 360);
            this.txtLog.Multiline = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(560, 100);
            // 
            // FormMain
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(760, 500);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.labelKey);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.groupDES);
            this.Controls.Add(this.groupRSA);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.txtLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ỨNG DỤNG MÃ HÓA RSA - DES (Tô Duy Tài - 0257)";
            this.groupDES.ResumeLayout(false);
            this.groupRSA.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.GroupBox groupDES;
        private System.Windows.Forms.Button btnEncryptDES;
        private System.Windows.Forms.Button btnDecryptDES;
        private System.Windows.Forms.GroupBox groupRSA;
        private System.Windows.Forms.Button btnGenRSA;
        private System.Windows.Forms.Button btnEncryptRSA;
        private System.Windows.Forms.Button btnDecryptRSA;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.TextBox txtLog;
    }
}
