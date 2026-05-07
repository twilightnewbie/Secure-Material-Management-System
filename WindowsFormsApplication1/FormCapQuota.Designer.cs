using System.Drawing;
using System.Windows.Forms;
using System;

namespace WindowsFormsApplication1
{
    partial class FormCapQuota
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblUser;
        private Label lblQuota;
        private Label lblTS;
        private TextBox txtUser;
        private TextBox txtQuota;
        private TextBox txtTablespace;
        private Button btnCap;
        private Button btnUnlimited;

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblUser = new Label();
            this.lblQuota = new Label();
            this.lblTS = new Label();
            this.txtUser = new TextBox();
            this.txtQuota = new TextBox();
            this.txtTablespace = new TextBox();
            this.btnCap = new Button();
            this.btnUnlimited = new Button();
            this.SuspendLayout();

            this.lblTitle.Text = "CẤP QUOTA CHO USER";
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(80, 20);
            this.lblTitle.AutoSize = true;

            this.lblUser.Text = "User:";
            this.lblUser.Location = new Point(40, 80);
            this.lblUser.AutoSize = true;

            this.lblTS.Text = "Tablespace:";
            this.lblTS.Location = new Point(40, 120);
            this.lblTS.AutoSize = true;

            this.lblQuota.Text = "Quota (MB):";
            this.lblQuota.Location = new Point(40, 160);
            this.lblQuota.AutoSize = true;

            this.txtUser.Location = new Point(150, 75);
            this.txtUser.Width = 150;

            this.txtTablespace.Location = new Point(150, 115);
            this.txtTablespace.Width = 150;
            this.txtTablespace.Text = "QLVT_TS";

            this.txtQuota.Location = new Point(150, 155);
            this.txtQuota.Width = 150;

            this.btnCap.Text = "Cấp quota";
            this.btnCap.Location = new Point(50, 210);
            this.btnCap.Size = new Size(100, 35);
            this.btnCap.Click += new EventHandler(this.btnCap_Click);

            this.btnUnlimited.Text = "UNLIMITED";
            this.btnUnlimited.Location = new Point(180, 210);
            this.btnUnlimited.Size = new Size(120, 35);
            this.btnUnlimited.Click += new EventHandler(this.btnUnlimited_Click);

            this.ClientSize = new Size(350, 300);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblTS);
            this.Controls.Add(this.lblQuota);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtQuota);
            this.Controls.Add(this.txtTablespace);
            this.Controls.Add(this.btnCap);
            this.Controls.Add(this.btnUnlimited);

            this.Name = "FormCapQuota";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Cấp quota";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
