namespace WindowsFormsApplication1
{
    partial class frmdau
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeaderInfo = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();

            // panelSidebar
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 70);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(260, 780);
            this.panelSidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSidebar_Paint);

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.panelHeader.Controls.Add(this.lblHeaderInfo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 70;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);

            // lblHeaderInfo
            this.lblHeaderInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblHeaderInfo.ForeColor = System.Drawing.Color.Aqua;
            this.lblHeaderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHeaderInfo.Width = 600;

            // panelContent
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.BackColor = System.Drawing.Color.WhiteSmoke;

            // frmdau
            this.ClientSize = new System.Drawing.Size(1300, 850);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmdau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HỆ THỐNG QUẢN TRỊ & BẢO MẬT ORACLE";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frmdau_FormClosed);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderInfo;
        private System.Windows.Forms.Panel panelContent;
    }
}