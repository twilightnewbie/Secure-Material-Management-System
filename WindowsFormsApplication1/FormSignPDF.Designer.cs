namespace WindowsFormsApplication1
{
    partial class FormSignPDF
    {
        private System.ComponentModel.IContainer components = null;

        // Các GroupBox để phân vùng giao diện
        private System.Windows.Forms.GroupBox grpKey;
        private System.Windows.Forms.GroupBox grpSignPDF;
        private System.Windows.Forms.GroupBox grpRSAFile;

        // Các Control cho phần Key
        private System.Windows.Forms.Label lblPrivateKey;
        private System.Windows.Forms.TextBox txtSign_PrivateKey;
        private System.Windows.Forms.Label lblPublicKey;
        private System.Windows.Forms.TextBox txtVerify_PublicKey;
        private System.Windows.Forms.Button btnGenKey;

        // Các Control cho phần Ký PDF
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Label lblSignStatus;
        private System.Windows.Forms.TextBox txtSign_Result;
        private System.Windows.Forms.Label lblResult;

        // Các Control cho phần Mã hóa RSA File
        private System.Windows.Forms.Label lblRSAIn;
        private System.Windows.Forms.TextBox txtRSA_InputFile;
        private System.Windows.Forms.Label lblRSAOut;
        private System.Windows.Forms.TextBox txtRSA_OutputFile;
        private System.Windows.Forms.Button btnRSA_DecryptFile;

        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpKey = new System.Windows.Forms.GroupBox();
            this.lblPrivateKey = new System.Windows.Forms.Label();
            this.txtSign_PrivateKey = new System.Windows.Forms.TextBox();
            this.lblPublicKey = new System.Windows.Forms.Label();
            this.txtVerify_PublicKey = new System.Windows.Forms.TextBox();
            this.btnGenKey = new System.Windows.Forms.Button();
            this.grpSignPDF = new System.Windows.Forms.GroupBox();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.lblSignStatus = new System.Windows.Forms.Label();
            this.txtSign_Result = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.grpRSAFile = new System.Windows.Forms.GroupBox();
            this.lblRSAIn = new System.Windows.Forms.Label();
            this.txtRSA_InputFile = new System.Windows.Forms.TextBox();
            this.lblRSAOut = new System.Windows.Forms.Label();
            this.txtRSA_OutputFile = new System.Windows.Forms.TextBox();
            this.btnRSA_DecryptFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.grpKey.SuspendLayout();
            this.grpSignPDF.SuspendLayout();
            this.grpRSAFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpKey
            // 
            this.grpKey.Controls.Add(this.lblPrivateKey);
            this.grpKey.Controls.Add(this.txtSign_PrivateKey);
            this.grpKey.Controls.Add(this.lblPublicKey);
            this.grpKey.Controls.Add(this.txtVerify_PublicKey);
            this.grpKey.Controls.Add(this.btnGenKey);
            this.grpKey.Location = new System.Drawing.Point(12, 12);
            this.grpKey.Name = "grpKey";
            this.grpKey.Size = new System.Drawing.Size(760, 160);
            this.grpKey.TabIndex = 0;
            this.grpKey.TabStop = false;
            this.grpKey.Text = "1. Quản lý RSA Key (XML Format)";
            // 
            // lblPrivateKey
            // 
            this.lblPrivateKey.Location = new System.Drawing.Point(15, 25);
            this.lblPrivateKey.Name = "lblPrivateKey";
            this.lblPrivateKey.Size = new System.Drawing.Size(79, 23);
            this.lblPrivateKey.TabIndex = 0;
            this.lblPrivateKey.Text = "Private Key:";
            // 
            // txtSign_PrivateKey
            // 
            this.txtSign_PrivateKey.Location = new System.Drawing.Point(100, 22);
            this.txtSign_PrivateKey.Multiline = true;
            this.txtSign_PrivateKey.Name = "txtSign_PrivateKey";
            this.txtSign_PrivateKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSign_PrivateKey.Size = new System.Drawing.Size(530, 60);
            this.txtSign_PrivateKey.TabIndex = 1;
            // 
            // lblPublicKey
            // 
            this.lblPublicKey.Location = new System.Drawing.Point(15, 95);
            this.lblPublicKey.Name = "lblPublicKey";
            this.lblPublicKey.Size = new System.Drawing.Size(79, 23);
            this.lblPublicKey.TabIndex = 2;
            this.lblPublicKey.Text = "Public Key:";
            // 
            // txtVerify_PublicKey
            // 
            this.txtVerify_PublicKey.Location = new System.Drawing.Point(100, 92);
            this.txtVerify_PublicKey.Multiline = true;
            this.txtVerify_PublicKey.Name = "txtVerify_PublicKey";
            this.txtVerify_PublicKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVerify_PublicKey.Size = new System.Drawing.Size(530, 60);
            this.txtVerify_PublicKey.TabIndex = 3;
            // 
            // btnGenKey
            // 
            this.btnGenKey.Location = new System.Drawing.Point(645, 22);
            this.btnGenKey.Name = "btnGenKey";
            this.btnGenKey.Size = new System.Drawing.Size(100, 130);
            this.btnGenKey.TabIndex = 4;
            this.btnGenKey.Text = "Tạo Cặp Khóa";
            this.btnGenKey.Click += new System.EventHandler(this.btnGenKey_Click);
            // 
            // grpSignPDF
            // 
            this.grpSignPDF.Controls.Add(this.lblMaHD);
            this.grpSignPDF.Controls.Add(this.txtMaHD);
            this.grpSignPDF.Controls.Add(this.lblFile);
            this.grpSignPDF.Controls.Add(this.txtFile);
            this.grpSignPDF.Controls.Add(this.btnBrowse);
            this.grpSignPDF.Controls.Add(this.btnSign);
            this.grpSignPDF.Controls.Add(this.btnVerify);
            this.grpSignPDF.Controls.Add(this.lblSignStatus);
            this.grpSignPDF.Controls.Add(this.txtSign_Result);
            this.grpSignPDF.Controls.Add(this.lblResult);
            this.grpSignPDF.Location = new System.Drawing.Point(12, 180);
            this.grpSignPDF.Name = "grpSignPDF";
            this.grpSignPDF.Size = new System.Drawing.Size(760, 180);
            this.grpSignPDF.TabIndex = 1;
            this.grpSignPDF.TabStop = false;
            this.grpSignPDF.Text = "2. Ký số PDF & Oracle Database";
            // 
            // lblMaHD
            // 
            this.lblMaHD.Location = new System.Drawing.Point(15, 25);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(100, 23);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Text = "Mã hóa đơn:";
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(121, 26);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(200, 22);
            this.txtMaHD.TabIndex = 1;
            // 
            // lblFile
            // 
            this.lblFile.Location = new System.Drawing.Point(15, 55);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(100, 23);
            this.lblFile.TabIndex = 2;
            this.lblFile.Text = "File PDF:";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(121, 52);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(530, 22);
            this.txtFile.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(657, 52);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Chọn File";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnSign
            // 
            this.btnSign.BackColor = System.Drawing.Color.LightBlue;
            this.btnSign.Location = new System.Drawing.Point(100, 85);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(120, 35);
            this.btnSign.TabIndex = 5;
            this.btnSign.Text = "Ký & Lưu DB";
            this.btnSign.UseVisualStyleBackColor = false;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(230, 85);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(120, 35);
            this.btnVerify.TabIndex = 6;
            this.btnVerify.Text = "Verify Chữ Ký";
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // lblSignStatus
            // 
            this.lblSignStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblSignStatus.Location = new System.Drawing.Point(365, 95);
            this.lblSignStatus.Name = "lblSignStatus";
            this.lblSignStatus.Size = new System.Drawing.Size(380, 20);
            this.lblSignStatus.TabIndex = 7;
            this.lblSignStatus.Text = "Trạng thái: Đợi kiểm tra...";
            // 
            // txtSign_Result
            // 
            this.txtSign_Result.Location = new System.Drawing.Point(100, 132);
            this.txtSign_Result.Name = "txtSign_Result";
            this.txtSign_Result.ReadOnly = true;
            this.txtSign_Result.Size = new System.Drawing.Size(645, 22);
            this.txtSign_Result.TabIndex = 8;
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(15, 135);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(100, 23);
            this.lblResult.TabIndex = 9;
            this.lblResult.Text = "Chữ ký (B64):";
            // 
            // grpRSAFile
            // 
            this.grpRSAFile.Controls.Add(this.lblRSAIn);
            this.grpRSAFile.Controls.Add(this.txtRSA_InputFile);
            this.grpRSAFile.Controls.Add(this.lblRSAOut);
            this.grpRSAFile.Controls.Add(this.txtRSA_OutputFile);
            this.grpRSAFile.Controls.Add(this.btnRSA_DecryptFile);
            this.grpRSAFile.Location = new System.Drawing.Point(12, 370);
            this.grpRSAFile.Name = "grpRSAFile";
            this.grpRSAFile.Size = new System.Drawing.Size(760, 100);
            this.grpRSAFile.TabIndex = 2;
            this.grpRSAFile.TabStop = false;
            this.grpRSAFile.Text = "3. Mã hóa & Giải mã File (RSA thuần - File < 214 bytes)";
            // 
            // lblRSAIn
            // 
            this.lblRSAIn.Location = new System.Drawing.Point(15, 25);
            this.lblRSAIn.Name = "lblRSAIn";
            this.lblRSAIn.Size = new System.Drawing.Size(79, 23);
            this.lblRSAIn.TabIndex = 0;
            this.lblRSAIn.Text = "File nguồn:";
            // 
            // txtRSA_InputFile
            // 
            this.txtRSA_InputFile.Location = new System.Drawing.Point(100, 22);
            this.txtRSA_InputFile.Name = "txtRSA_InputFile";
            this.txtRSA_InputFile.Size = new System.Drawing.Size(250, 22);
            this.txtRSA_InputFile.TabIndex = 1;
            // 
            // lblRSAOut
            // 
            this.lblRSAOut.Location = new System.Drawing.Point(370, 25);
            this.lblRSAOut.Name = "lblRSAOut";
            this.lblRSAOut.Size = new System.Drawing.Size(59, 23);
            this.lblRSAOut.TabIndex = 2;
            this.lblRSAOut.Text = "File đích:";
            // 
            // txtRSA_OutputFile
            // 
            this.txtRSA_OutputFile.Location = new System.Drawing.Point(435, 22);
            this.txtRSA_OutputFile.Name = "txtRSA_OutputFile";
            this.txtRSA_OutputFile.Size = new System.Drawing.Size(310, 22);
            this.txtRSA_OutputFile.TabIndex = 3;
            // 
            // btnRSA_DecryptFile
            // 
            this.btnRSA_DecryptFile.Location = new System.Drawing.Point(230, 55);
            this.btnRSA_DecryptFile.Name = "btnRSA_DecryptFile";
            this.btnRSA_DecryptFile.Size = new System.Drawing.Size(120, 30);
            this.btnRSA_DecryptFile.TabIndex = 5;
            this.btnRSA_DecryptFile.Text = "Giải mã File";
            this.btnRSA_DecryptFile.Click += new System.EventHandler(this.btnRSA_DecryptFile_Click);
            // 
            // FormSignPDF
            // 
            this.ClientSize = new System.Drawing.Size(779, 566);
            this.Controls.Add(this.grpKey);
            this.Controls.Add(this.grpSignPDF);
            this.Controls.Add(this.grpRSAFile);
            this.Name = "FormSignPDF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Ký số PDF & Mã hóa RSA (Oracle DB)";
            this.grpKey.ResumeLayout(false);
            this.grpKey.PerformLayout();
            this.grpSignPDF.ResumeLayout(false);
            this.grpSignPDF.PerformLayout();
            this.grpRSAFile.ResumeLayout(false);
            this.grpRSAFile.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}