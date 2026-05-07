namespace WindowsFormsApplication1
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtNewUser;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtNewUser = new System.Windows.Forms.TextBox();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(60, 50);
            this.lblUser.Text = "Tên user:";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(60, 90);
            this.lblPass.Text = "Mật khẩu:";
            // 
            // txtNewUser
            // 
            this.txtNewUser.Location = new System.Drawing.Point(160, 47);
            this.txtNewUser.Size = new System.Drawing.Size(210, 22);
            // 
            // txtNewPass
            // 
            this.txtNewPass.Location = new System.Drawing.Point(160, 87);
            this.txtNewPass.Size = new System.Drawing.Size(210, 22);
            this.txtNewPass.UseSystemPasswordChar = true;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(160, 130);
            this.btnCreate.Text = "Tạo User";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(275, 130);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormRegister
            // 
            this.ClientSize = new System.Drawing.Size(450, 200);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtNewPass);
            this.Controls.Add(this.txtNewUser);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.lblUser);
            this.Text = "Đăng ký Oracle User";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
