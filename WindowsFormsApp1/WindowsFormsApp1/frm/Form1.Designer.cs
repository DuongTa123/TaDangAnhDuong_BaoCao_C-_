
namespace WindowsFormsApp1
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MenuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBoPhan = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViTriCongViec = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNghiPhep = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDiemDanh = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDuAnCongViec = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain = new System.Windows.Forms.Panel();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 33);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1182, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuNhanVien,
            this.MenuBoPhan,
            this.MenuViTriCongViec,
            this.MenuLuong,
            this.MenuNghiPhep,
            this.MenuDiemDanh,
            this.MenuDuAnCongViec});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1182, 33);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip2";
            // 
            // MenuNhanVien
            // 
            this.MenuNhanVien.Checked = true;
            this.MenuNhanVien.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuNhanVien.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuNhanVien.Image = global::WindowsFormsApp1.Properties.Resources.Nhanvien;
            this.MenuNhanVien.Name = "MenuNhanVien";
            this.MenuNhanVien.Size = new System.Drawing.Size(139, 29);
            this.MenuNhanVien.Text = "Nhân Viên";
            this.MenuNhanVien.Click += new System.EventHandler(this.MenuNhanVien_Click);
            // 
            // MenuBoPhan
            // 
            this.MenuBoPhan.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuBoPhan.Image = global::WindowsFormsApp1.Properties.Resources.BoPhan;
            this.MenuBoPhan.Name = "MenuBoPhan";
            this.MenuBoPhan.Size = new System.Drawing.Size(120, 29);
            this.MenuBoPhan.Text = "Bộ Phận";
            this.MenuBoPhan.Click += new System.EventHandler(this.MenuBoPhan_Click);
            // 
            // MenuViTriCongViec
            // 
            this.MenuViTriCongViec.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuViTriCongViec.Image = global::WindowsFormsApp1.Properties.Resources.ViTriCViec;
            this.MenuViTriCongViec.Name = "MenuViTriCongViec";
            this.MenuViTriCongViec.Size = new System.Drawing.Size(187, 29);
            this.MenuViTriCongViec.Text = "Vị Trí Công Việc";
            this.MenuViTriCongViec.Click += new System.EventHandler(this.MenuViTriCongViec_Click);
            // 
            // MenuLuong
            // 
            this.MenuLuong.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuLuong.Image = global::WindowsFormsApp1.Properties.Resources.Luong;
            this.MenuLuong.Name = "MenuLuong";
            this.MenuLuong.Size = new System.Drawing.Size(105, 29);
            this.MenuLuong.Text = "Lương";
            this.MenuLuong.Click += new System.EventHandler(this.MenuLuong_Click);
            // 
            // MenuNghiPhep
            // 
            this.MenuNghiPhep.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuNghiPhep.Image = global::WindowsFormsApp1.Properties.Resources.YCNghiPhep;
            this.MenuNghiPhep.Name = "MenuNghiPhep";
            this.MenuNghiPhep.Size = new System.Drawing.Size(215, 29);
            this.MenuNghiPhep.Text = "Yêu Cầu Nghỉ Phép";
            this.MenuNghiPhep.Click += new System.EventHandler(this.MenuNghiPhep_Click);
            // 
            // MenuDiemDanh
            // 
            this.MenuDiemDanh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuDiemDanh.Image = global::WindowsFormsApp1.Properties.Resources.DiemDanh;
            this.MenuDiemDanh.Name = "MenuDiemDanh";
            this.MenuDiemDanh.Size = new System.Drawing.Size(142, 29);
            this.MenuDiemDanh.Text = "Điểm danh";
            this.MenuDiemDanh.Click += new System.EventHandler(this.MenuDiemDanh_Click);
            // 
            // MenuDuAnCongViec
            // 
            this.MenuDuAnCongViec.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuDuAnCongViec.Image = global::WindowsFormsApp1.Properties.Resources.DuAn;
            this.MenuDuAnCongViec.Name = "MenuDuAnCongViec";
            this.MenuDuAnCongViec.Size = new System.Drawing.Size(198, 29);
            this.MenuDuAnCongViec.Text = "Dự Án Công Việc";
            this.MenuDuAnCongViec.Click += new System.EventHandler(this.MenuDuAnCongViec_Click);
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 63);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1182, 511);
            this.panelMain.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 574);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem MenuBoPhan;
        private System.Windows.Forms.ToolStripMenuItem MenuViTriCongViec;
        private System.Windows.Forms.ToolStripMenuItem MenuLuong;
        private System.Windows.Forms.ToolStripMenuItem MenuNghiPhep;
        private System.Windows.Forms.ToolStripMenuItem MenuDiemDanh;
        private System.Windows.Forms.ToolStripMenuItem MenuDuAnCongViec;
        private System.Windows.Forms.Panel panelMain;
    }
}

