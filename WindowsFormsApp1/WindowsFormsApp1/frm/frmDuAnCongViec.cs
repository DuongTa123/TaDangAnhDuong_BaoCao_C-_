using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.frm
{
    public partial class frmDuAnCongViec : Form
    {
        public frmDuAnCongViec()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True");
        private void RefreshDataGridView()
        {
            SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True");
            {
                conn.Open();

                // Thực hiện truy vấn SQL để lấy dữ liệu từ cơ sở dữ liệu
                string query = "SELECT * FROM Projects ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Cập nhật bảng dgvQLS bằng dữ liệu mới
                        dgvDSDuAn.DataSource = dataTable;
                    }
                }
            }
        }
        private void dataQLDuAn()
        {
            conn.Open();
            string sql = "SELECT * FROM Projects";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dgvDSDuAn.DataSource = dt;
        }

        private void frmDuAnCongViec_Load(object sender, EventArgs e)
        {
            dataQLDuAn();
        }
        private bool IsMaDuAnExists(string maDuAn)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Projects WHERE ProjectID = @ProjectID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ProjectID", maDuAn);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maDuAn = tbMaDuAn.Text;
            if (IsMaDuAnExists(maDuAn))
            {
                MessageBox.Show("Mã Dự Án đã tồn tại!");
                return;
            }
            string tenDuAn = tbTenDuAn.Text;
            DateTime ngayBD = dtpNgayBD.Value;
            DateTime ngayKT = dtpNgayKT.Value;
            string moTa = tbMoTa.Text;
            string trangThai = cbTrangThai.Text;

            string query = "INSERT INTO Projects (ProjectID, ProjectName, Description, StartDate, EndDate, Status) " +
                                          "VALUES (@ProjectID, @ProjectName, @Description, @StartDate, @EndDate, @Status)";

            SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True");
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProjectID", maDuAn);
                cmd.Parameters.AddWithValue("@ProjectName", tenDuAn);
                cmd.Parameters.AddWithValue("@Description", moTa);
                cmd.Parameters.AddWithValue("@StartDate", ngayBD);
                cmd.Parameters.AddWithValue("@EndDate", ngayKT);
                cmd.Parameters.AddWithValue("@Status", trangThai);
                

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Dữ liệu đã được thêm vào cơ sở dữ liệu.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
                }
            }
            RefreshDataGridView();
        }
        private bool isEditing = false;
        private int editedRowIndex = -1;
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSDuAn.SelectedRows.Count > 0)
            {
                string maDuAn = dgvDSDuAn.SelectedRows[0].Cells["ProjectID"].Value.ToString();

                string query = "DELETE FROM Projects WHERE ProjectID = @ProjectID";
                using (SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProjectID", maDuAn);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Dữ liệu đã được xóa.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
                        }
                    }
                }

                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                if (dgvDSDuAn.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSDuAn.SelectedRows[0];
                    tbMaDuAn.Text = selectedRow.Cells["ProjectID"].Value.ToString();
                    tbTenDuAn.Text = selectedRow.Cells["ProjectName"].Value.ToString();
                    tbMoTa.Text = selectedRow.Cells["Description"].Value.ToString();
                    cbTrangThai.Text = selectedRow.Cells["Status"].Value.ToString();
                    dtpNgayBD.Text = selectedRow.Cells["StartDate"].Value.ToString();
                    dtpNgayKT.Text = selectedRow.Cells["EndDate"].Value.ToString();
                    
                    isEditing = true;
                    editedRowIndex = selectedRow.Index;
                    btnThem.Enabled = false;
                    btnSua.Text = "Lưu";
                }
            }
            else
            {
                string maDuAn = tbMaDuAn.Text;
                if (IsMaDuAnExists(maDuAn) && maDuAn != dgvDSDuAn.Rows[editedRowIndex].Cells["ProjectID"].Value.ToString())
                {
                    MessageBox.Show("Mã Dự Án đã tồn tại");
                    return;
                }
                string tenDuAn = tbTenDuAn.Text;
                DateTime ngayBD = dtpNgayBD.Value;
                DateTime ngayKT = dtpNgayKT.Value;
                string moTa = tbMoTa.Text;
                string trangThai = cbTrangThai.Text;

                string connString = @"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True";

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Projects SET ProjectID = @ProjectID, ProjectName = @ProjectName, Description = @Description, StartDate = @StartDate, EndDate = @EndDate, Status = @Status  WHERE ProjectID = @ProjectID_Old";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = maDuAn;
                            command.Parameters.Add("@ProjectID_Old", SqlDbType.Int).Value = dgvDSDuAn.Rows[editedRowIndex].Cells["ProjectID"].Value.ToString();
                            command.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = tenDuAn;
                            command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = moTa;
                            command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = ngayBD;
                            command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = ngayKT;
                            command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = trangThai;
                           
                            command.ExecuteNonQuery();

                            MessageBox.Show("Dữ liệu đã được cập nhật.");
                        }
                        dataQLDuAn();
                        tbMaDuAn.Clear();
                        tbTenDuAn.Clear();
                        tbMoTa.Clear();
                        dtpNgayBD.Value = DateTime.Now;
                        dtpNgayKT.Value = DateTime.Now;
                        isEditing = false;
                        btnThem.Enabled = true;
                        btnSua.Text = "Sửa";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            tbTimKiem.Clear();
            tbMaDuAn.Clear();
            tbTenDuAn.Clear();
            tbMoTa.Clear();
            dtpNgayBD.Value = DateTime.Now;
            dtpNgayKT.Value = DateTime.Now;
            RefreshDataGridView();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = tbTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                string query = "SELECT * FROM Projects WHERE ProjectID LIKE @Keyword OR ProjectName LIKE @Keyword OR Status LIKE @Keyword";
                using (SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                        try
                        {
                            conn.Open();
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                dgvDSDuAn.DataSource = dataTable;

                                if (dataTable.Rows.Count == 0)
                                {
                                    MessageBox.Show("Không tìm thấy kết quả nào.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.");
            }
        }
    }
}
