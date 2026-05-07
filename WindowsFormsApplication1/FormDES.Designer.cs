namespace WindowsFormsApplication1
{
    partial class FormDES
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblOutput = new System.Windows.Forms.Label();

            // --- Các control mới cho phần File ---
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnEncryptFile = new System.Windows.Forms.Button();
            this.btnDecryptFile = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(140, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "MÃ HÓA DES";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblKey & txtKey
            // 
            this.lblKey.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKey.Location = new System.Drawing.Point(40, 60);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(120, 22);
            this.lblKey.Text = "Khóa (8 ký tự):";

            this.txtKey.Location = new System.Drawing.Point(160, 60);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(180, 22);
            this.txtKey.TabIndex = 2;

            // 
            // lblInput & txtInput
            // 
            this.lblInput.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInput.Location = new System.Drawing.Point(40, 95);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(150, 22);
            this.lblInput.Text = "Dữ liệu văn bản:";

            this.txtInput.Location = new System.Drawing.Point(40, 120);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(450, 60);
            this.txtInput.TabIndex = 1;

            // 
            // btnEncrypt & btnDecrypt (Text)
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(40, 190);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(100, 30);
            this.btnEncrypt.Text = "Mã hóa Text";
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);

            this.btnDecrypt.Location = new System.Drawing.Point(150, 190);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(100, 30);
            this.btnDecrypt.Text = "Giải mã Text";
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);

            // 
            // lblOutput & txtOutput
            // 
            this.lblOutput.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOutput.Location = new System.Drawing.Point(40, 230);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(150, 22);
            this.lblOutput.Text = "Kết quả Text:";

            this.txtOutput.Location = new System.Drawing.Point(40, 255);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(450, 60);
            this.txtOutput.TabIndex = 7;

            // --- PHẦN FILE (Mới thêm) ---

            // lblFile
            this.lblFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFile.Location = new System.Drawing.Point(40, 330);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(100, 22);
            this.lblFile.Text = "Chọn File:";

            // txtFilePath
            this.txtFilePath.Location = new System.Drawing.Point(120, 328);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(290, 22);
            this.txtFilePath.ReadOnly = true;

            // btnBrowse
            this.btnBrowse.Location = new System.Drawing.Point(420, 326);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(70, 25);
            this.btnBrowse.Text = "...";
            this.btnBrowse.Click += new System.EventHandler(this.btnChooseFile_Click);

            // btnEncryptFile
            this.btnEncryptFile.Location = new System.Drawing.Point(40, 360);
            this.btnEncryptFile.Name = "btnEncryptFile";
            this.btnEncryptFile.Size = new System.Drawing.Size(100, 30);
            this.btnEncryptFile.Text = "Mã hóa File";
            this.btnEncryptFile.Click += new System.EventHandler(this.btnEncryptFile_Click);

            // btnDecryptFile
            this.btnDecryptFile.Location = new System.Drawing.Point(150, 360);
            this.btnDecryptFile.Name = "btnDecryptFile";
            this.btnDecryptFile.Size = new System.Drawing.Size(100, 30);
            this.btnDecryptFile.Text = "Giải mã File";
            this.btnDecryptFile.Click += new System.EventHandler(this.btnDecryptFile_Click);

            // 
            // FormDES
            // 
            this.ClientSize = new System.Drawing.Size(530, 420); // Tăng chiều cao form
            this.Controls.Add(this.btnDecryptFile);
            this.Controls.Add(this.btnEncryptFile);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblTitle);
            this.Name = "FormDES";
            this.Text = "Mã hóa DES - Text & File";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblOutput;

        // Các biến thiếu đã được bổ sung:
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Button btnEncryptFile;
        private System.Windows.Forms.Button btnDecryptFile;
    }
}