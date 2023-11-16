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
    public partial class frmDiemDanh : Form
    {
        public frmDiemDanh()
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
                string query = "SELECT * FROM Attendances ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Cập nhật bảng dgvQLS bằng dữ liệu mới
                        dgvDSDiemDanh.DataSource = dataTable;
                    }
                }
            }
        }
        private void dataQLDiemDanh()
        {
            conn.Open();
            string sql = "SELECT * FROM Attendances";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dgvDSDiemDanh.DataSource = dt;
        }

        private void frmDiemDanh_Load(object sender, EventArgs e)
        {
            dataQLDiemDanh();
        }
        private bool IsMaDiemDanhExists(string maDDanh)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Attendances WHERE AttendanceID = @AttendanceID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AttendanceID", maDDanh);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maDDanh = tbMaDiemDanh.Text;
            if (IsMaDiemDanhExists(maDDanh))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại!");
                return;
            }
            string maNhanVien = tbMaNhanVien.Text;
            DateTime ngayDiemDanh = dtpNgayDiemDanh.Value;
            string trangThai = cbTrangThai.Text;

            string query = "INSERT INTO Attendances (AttendanceID, EmployeeID, AttendanceDate, Status) " +
                                          "VALUES (@AttendanceID, @EmployeeID, @AttendanceDate, @Status)";

            SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True");
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AttendanceID", maDDanh);
                cmd.Parameters.AddWithValue("@EmployeeID", maNhanVien);
                cmd.Parameters.AddWithValue("@AttendanceDate", ngayDiemDanh);
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
            if (dgvDSDiemDanh.SelectedRows.Count > 0)
            {
                string maDD = dgvDSDiemDanh.SelectedRows[0].Cells["AttendanceID"].Value.ToString();

                string query = "DELETE FROM Attendances WHERE AttendanceID = @AttendanceID";
                using (SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AttendanceID", maDD);
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
                if (dgvDSDiemDanh.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSDiemDanh.SelectedRows[0];
                    tbMaDiemDanh.Text = selectedRow.Cells["AttendanceID"].Value.ToString();
                    tbMaNhanVien.Text = selectedRow.Cells["EmployeeID"].Value.ToString();
                    dtpNgayDiemDanh.Text = selectedRow.Cells["AttendanceDate"].Value.ToString();
                    cbTrangThai.Text = selectedRow.Cells["Status"].Value.ToString();
                    
                    isEditing = true;
                    editedRowIndex = selectedRow.Index;
                    btnThem.Enabled = false;
                    btnSua.Text = "Lưu";
                }
            }
            else
            {
                string maDD = tbMaDiemDanh.Text;
                if (IsMaDiemDanhExists(maDD) && maDD != dgvDSDiemDanh.Rows[editedRowIndex].Cells["AttendanceID"].Value.ToString())
                {
                    MessageBox.Show("Mã điểm danh đã tồn tại");
                    return;
                }
                string maNhanVien = tbMaNhanVien.Text;
                DateTime ngayDiemDanh = dtpNgayDiemDanh.Value;
                string trangThai = cbTrangThai.Text;

                string connString = @"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True";

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Attendances SET AttendanceID = @AttendanceID, EmployeeID = @EmployeeID, AttendanceDate = @AttendanceDate, Status = @Status WHERE AttendanceID = @AttendanceID_Old";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@AttendanceID", SqlDbType.Int).Value = maDD;
                            command.Parameters.Add("@AttendanceID_Old", SqlDbType.Int).Value = dgvDSDiemDanh.Rows[editedRowIndex].Cells["AttendanceID"].Value.ToString();
                            command.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = maNhanVien;
                            command.Parameters.Add("@AttendanceDate", SqlDbType.DateTime).Value = ngayDiemDanh;
                            command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = trangThai;
                            
                            command.ExecuteNonQuery();

                            MessageBox.Show("Dữ liệu đã được cập nhật.");
                        }
                        dataQLDiemDanh();
                        tbMaDiemDanh.Clear();
                        tbMaNhanVien.Clear();
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = tbTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                string query = "SELECT * FROM Attendances WHERE AttendanceID LIKE @Keyword OR EmployeeID LIKE @Keyword OR Status LIKE @Keyword";
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

                                dgvDSDiemDanh.DataSource = dataTable;

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

        private void btnReload_Click(object sender, EventArgs e)
        {
            tbTimKiem.Clear();
            tbMaNhanVien.Clear();
            tbMaDiemDanh.Clear();
            RefreshDataGridView();
        }
    }
}
