using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.frm;

namespace WindowsFormsApp1
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            frmLogin loginForm = new frmLogin();
            loginForm.ShowDialog();
        }
        private void MenuNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien frmNVien = new frmNhanVien();


            frmNVien.TopLevel = false;
            frmNVien.Parent = this;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(frmNVien);
            frmNVien.Location = new Point((panelMain.Width - frmNVien.Width) / 2, (panelMain.Height - frmNVien.Height) / 2);
            frmNVien.Show();
           
          
        }

        private void MenuBoPhan_Click(object sender, EventArgs e)
        {
            frmBoPhan frmBPhan = new frmBoPhan();


            frmBPhan.TopLevel = false;
            frmBPhan.Parent = this;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(frmBPhan);
            frmBPhan.Location = new Point((panelMain.Width - frmBPhan.Width) / 2, (panelMain.Height - frmBPhan.Height) / 2);
            frmBPhan.Show();
        }

        private void MenuViTriCongViec_Click(object sender, EventArgs e)
        {
            frmViTriCongViec frmVTCViec = new frmViTriCongViec();


            frmVTCViec.TopLevel = false;
            frmVTCViec.Parent = this;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(frmVTCViec);
            frmVTCViec.Location = new Point((panelMain.Width - frmVTCViec.Width) / 2, (panelMain.Height - frmVTCViec.Height) / 2);
            frmVTCViec.Show();
        }

        private void MenuLuong_Click(object sender, EventArgs e)
        {
            frmLuong frmTTinLuong = new frmLuong();


            frmTTinLuong.TopLevel = false;
            frmTTinLuong.Parent = this;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(frmTTinLuong);
            frmTTinLuong.Location = new Point((panelMain.Width - frmTTinLuong.Width) / 2, (panelMain.Height - frmTTinLuong.Height) / 2);
            frmTTinLuong.Show();
        }

        private void MenuNghiPhep_Click(object sender, EventArgs e)
        {
            frmYCNghiPhep frmNghiPhep = new frmYCNghiPhep();


            frmNghiPhep.TopLevel = false;
            frmNghiPhep.Parent = this;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(frmNghiPhep);
            frmNghiPhep.Location = new Point((panelMain.Width - frmNghiPhep.Width) / 2, (panelMain.Height - frmNghiPhep.Height) / 2);
            frmNghiPhep.Show();
        }

        private void MenuDiemDanh_Click(object sender, EventArgs e)
        {
            frmDiemDanh frmDD = new frmDiemDanh();


            frmDD.TopLevel = false;
            frmDD.Parent = this;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(frmDD);
            frmDD.Location = new Point((panelMain.Width - frmDD.Width) / 2, (panelMain.Height - frmDD.Height) / 2);
            frmDD.Show();
        }

        private void MenuDuAnCongViec_Click(object sender, EventArgs e)
        {
            frmDuAnCongViec frmDACV = new frmDuAnCongViec();


            frmDACV.TopLevel = false;
            frmDACV.Parent = this;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(frmDACV);
            frmDACV.Location = new Point((panelMain.Width - frmDACV.Width) / 2, (panelMain.Height - frmDACV.Height) / 2);
            frmDACV.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đóng ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
