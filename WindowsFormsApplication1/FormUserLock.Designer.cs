namespace WindowsFormsApplication1
{
    partial class FormUserLock
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
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.dgvPrivileges = new System.Windows.Forms.DataGridView();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.cbProfiles = new System.Windows.Forms.ComboBox();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnGrant = new System.Windows.Forms.Button();
            this.btnRevoke = new System.Windows.Forms.Button();
            this.btnApplyProfile = new System.Windows.Forms.Button();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.chkInsert = new System.Windows.Forms.CheckBox();
            this.chkUpdate = new System.Windows.Forms.CheckBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivileges)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUsers
            // 
            this.dgvUsers.ColumnHeadersHeight = 29;
            this.dgvUsers.Location = new System.Drawing.Point(20, 60);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(313, 260);
            this.dgvUsers.TabIndex = 1;
            // 
            // dgvPrivileges
            // 
            this.dgvPrivileges.ColumnHeadersHeight = 29;
            this.dgvPrivileges.Location = new System.Drawing.Point(350, 180);
            this.dgvPrivileges.Name = "dgvPrivileges";
            this.dgvPrivileges.ReadOnly = true;
            this.dgvPrivileges.RowHeadersWidth = 51;
            this.dgvPrivileges.Size = new System.Drawing.Size(370, 140);
            this.dgvPrivileges.TabIndex = 11;
            // 
            // cbUsers
            // 
            this.cbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsers.Location = new System.Drawing.Point(20, 20);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(220, 24);
            this.cbUsers.TabIndex = 0;
            // 
            // cbProfiles
            // 
            this.cbProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProfiles.Location = new System.Drawing.Point(350, 20);
            this.cbProfiles.Name = "cbProfiles";
            this.cbProfiles.Size = new System.Drawing.Size(200, 24);
            this.cbProfiles.TabIndex = 2;
            // 
            // cbTables
            // 
            this.cbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTables.Location = new System.Drawing.Point(350, 80);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(200, 24);
            this.cbTables.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(20, 330);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Thêm User";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnLock
            // 
            this.btnLock.Location = new System.Drawing.Point(120, 330);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(75, 23);
            this.btnLock.TabIndex = 13;
            this.btnLock.Text = "Khóa";
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(200, 330);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(75, 23);
            this.btnUnlock.TabIndex = 14;
            this.btnUnlock.Text = "Mở Khóa";
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(300, 330);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Xóa User";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(400, 330);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 16;
            this.btnReload.Text = "Tải lại";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnGrant
            // 
            this.btnGrant.Location = new System.Drawing.Point(350, 130);
            this.btnGrant.Name = "btnGrant";
            this.btnGrant.Size = new System.Drawing.Size(100, 35);
            this.btnGrant.TabIndex = 9;
            this.btnGrant.Text = "Cấp quyền";
            this.btnGrant.Click += new System.EventHandler(this.btnGrant_Click);
            // 
            // btnRevoke
            // 
            this.btnRevoke.Location = new System.Drawing.Point(460, 130);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(100, 35);
            this.btnRevoke.TabIndex = 10;
            this.btnRevoke.Text = "Thu hồi";
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            // 
            // btnApplyProfile
            // 
            this.btnApplyProfile.Location = new System.Drawing.Point(570, 15);
            this.btnApplyProfile.Name = "btnApplyProfile";
            this.btnApplyProfile.Size = new System.Drawing.Size(150, 35);
            this.btnApplyProfile.TabIndex = 3;
            this.btnApplyProfile.Text = "Áp dụng Profile";
            this.btnApplyProfile.Click += new System.EventHandler(this.btnApplyProfile_Click);
            // 
            // chkSelect
            // 
            this.chkSelect.Location = new System.Drawing.Point(570, 80);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(104, 24);
            this.chkSelect.TabIndex = 5;
            this.chkSelect.Text = "SELECT";
            // 
            // chkInsert
            // 
            this.chkInsert.Location = new System.Drawing.Point(680, 80);
            this.chkInsert.Name = "chkInsert";
            this.chkInsert.Size = new System.Drawing.Size(104, 24);
            this.chkInsert.TabIndex = 6;
            this.chkInsert.Text = "INSERT";
            // 
            // chkUpdate
            // 
            this.chkUpdate.Location = new System.Drawing.Point(570, 110);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(104, 24);
            this.chkUpdate.TabIndex = 7;
            this.chkUpdate.Text = "UPDATE";
            // 
            // chkDelete
            // 
            this.chkDelete.Location = new System.Drawing.Point(680, 110);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(104, 24);
            this.chkDelete.TabIndex = 8;
            this.chkDelete.Text = "DELETE";
            // 
            // FormUserLock
            // 
            this.ClientSize = new System.Drawing.Size(990, 380);
            this.Controls.Add(this.cbUsers);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.cbProfiles);
            this.Controls.Add(this.btnApplyProfile);
            this.Controls.Add(this.cbTables);
            this.Controls.Add(this.chkSelect);
            this.Controls.Add(this.chkInsert);
            this.Controls.Add(this.chkUpdate);
            this.Controls.Add(this.chkDelete);
            this.Controls.Add(this.btnGrant);
            this.Controls.Add(this.btnRevoke);
            this.Controls.Add(this.dgvPrivileges);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReload);
            this.Name = "FormUserLock";
            this.Text = "Quản lý User & Phân quyền";
            this.Load += new System.EventHandler(this.FormUserLock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivileges)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridView dgvPrivileges;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.ComboBox cbProfiles;
        private System.Windows.Forms.ComboBox cbTables;
        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.CheckBox chkInsert;
        private System.Windows.Forms.CheckBox chkUpdate;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnGrant;
        private System.Windows.Forms.Button btnRevoke;
        private System.Windows.Forms.Button btnApplyProfile;
    }
}
