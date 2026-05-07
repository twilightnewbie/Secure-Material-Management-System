namespace WindowsFormsApplication1
{
    partial class FormQuotaManager
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

        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.ComboBox cbTablespaces;
        private System.Windows.Forms.ComboBox cbQuota;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.DataGridView dgvQuota;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblTS;
        private System.Windows.Forms.Label lblQuota;
        private System.Windows.Forms.Label lblTitle;

        private void InitializeComponent()
        {
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.cbTablespaces = new System.Windows.Forms.ComboBox();
            this.cbQuota = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.dgvQuota = new System.Windows.Forms.DataGridView();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblTS = new System.Windows.Forms.Label();
            this.lblQuota = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuota)).BeginInit();
            this.SuspendLayout();
            // 
            // cbUsers
            // 
            this.cbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsers.Location = new System.Drawing.Point(150, 85);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(250, 24);
            this.cbUsers.TabIndex = 2;
            // 
            // cbTablespaces
            // 
            this.cbTablespaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTablespaces.Location = new System.Drawing.Point(150, 135);
            this.cbTablespaces.Name = "cbTablespaces";
            this.cbTablespaces.Size = new System.Drawing.Size(250, 24);
            this.cbTablespaces.TabIndex = 4;
            // 
            // cbQuota
            // 
            this.cbQuota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuota.Items.AddRange(new object[] {
            "10M",
            "50M",
            "100M",
            "500M",
            "UNLIMITED"});
            this.cbQuota.Location = new System.Drawing.Point(150, 185);
            this.cbQuota.Name = "cbQuota";
            this.cbQuota.Size = new System.Drawing.Size(250, 24);
            this.cbQuota.TabIndex = 6;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnApply.Location = new System.Drawing.Point(150, 240);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(150, 40);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "✔ Cấp quota";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // dgvQuota
            // 
            this.dgvQuota.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuota.ColumnHeadersHeight = 29;
            this.dgvQuota.Location = new System.Drawing.Point(20, 300);
            this.dgvQuota.Name = "dgvQuota";
            this.dgvQuota.ReadOnly = true;
            this.dgvQuota.RowHeadersWidth = 51;
            this.dgvQuota.Size = new System.Drawing.Size(450, 200);
            this.dgvQuota.TabIndex = 8;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(50, 90);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(39, 16);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "User:";
            // 
            // lblTS
            // 
            this.lblTS.AutoSize = true;
            this.lblTS.Location = new System.Drawing.Point(50, 140);
            this.lblTS.Name = "lblTS";
            this.lblTS.Size = new System.Drawing.Size(84, 16);
            this.lblTS.TabIndex = 3;
            this.lblTS.Text = "Tablespace:";
            // 
            // lblQuota
            // 
            this.lblQuota.AutoSize = true;
            this.lblQuota.Location = new System.Drawing.Point(50, 190);
            this.lblQuota.Name = "lblQuota";
            this.lblQuota.Size = new System.Drawing.Size(46, 16);
            this.lblQuota.TabIndex = 5;
            this.lblQuota.Text = "Quota:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(110, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(415, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ TABLESPACE - QUOTA";
            // 
            // FormQuotaManager
            // 
            this.ClientSize = new System.Drawing.Size(675, 520);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.cbUsers);
            this.Controls.Add(this.lblTS);
            this.Controls.Add(this.cbTablespaces);
            this.Controls.Add(this.lblQuota);
            this.Controls.Add(this.cbQuota);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.dgvQuota);
            this.Name = "FormQuotaManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quota Manager";
            this.Load += new System.EventHandler(this.FormQuotaManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuota)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
