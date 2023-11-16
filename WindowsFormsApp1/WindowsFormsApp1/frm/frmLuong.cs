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
    public partial class frmLuong : Form
    {
        public frmLuong()
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
                string query = "SELECT * FROM Salaries ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Cập nhật bảng dgvQLS bằng dữ liệu mới
                        dgvDSLuong.DataSource = dataTable;
                    }
                }
            }
        }
        private void dataQLLuong()
        {
            conn.Open();
            string sql = "SELECT * FROM Salaries";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dgvDSLuong.DataSource = dt;
        }
        private void frmLuong_Load(object sender, EventArgs e)
        {
            dataQLLuong();
        }
        private bool IsMaLuongExists(string maLuong)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Salaries WHERE SalaryID = @SalaryID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SalaryID", maLuong);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maLuong = tbMaLuong.Text;
            if (IsMaLuongExists(maLuong))
            {
                MessageBox.Show("Mã Lương đã tồn tại!");
                return;
            }
            string maNhanVien = tbMaNhanVien.Text;
            string tongLuongNhan = tbTongLuong.Text;
            string soNgayLuong = cbTongNgayLuong.Text;

            string query = "INSERT INTO Salaries (SalaryID, EmployeeID, SalaryAmount, SalaryDate ) VALUES (@SalaryID, @EmployeeID, @SalaryAmount, @SalaryDate)";

            SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True");
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@SalaryID", maLuong);
                cmd.Parameters.AddWithValue("@EmployeeID", maNhanVien);
                cmd.Parameters.AddWithValue("@SalaryAmount", tongLuongNhan);
                cmd.Parameters.AddWithValue("@SalaryDate", soNgayLuong);

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
            if (dgvDSLuong.SelectedRows.Count > 0)
            {
                string maLuong = dgvDSLuong.SelectedRows[0].Cells["SalaryID"].Value.ToString();

                string query = "DELETE FROM Salaries WHERE SalaryID = @SalaryID";
                using (SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SalaryID", maLuong);
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
                MessageBox.Show("Vui lòng chọn bộ phận để xóa.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                if (dgvDSLuong.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSLuong.SelectedRows[0];
                    tbMaLuong.Text = selectedRow.Cells["SalaryID"].Value.ToString();
                    tbMaNhanVien.Text = selectedRow.Cells["EmployeeID"].Value.ToString();
                    tbTongLuong.Text = selectedRow.Cells["SalaryAmount"].Value.ToString();
                    cbTongNgayLuong.Text = selectedRow.Cells["SalaryDate"].Value.ToString();

                    isEditing = true;
                    editedRowIndex = selectedRow.Index;
                    btnThem.Enabled = false;
                    btnSua.Text = "Lưu";
                }
            }
            else
            {
                string maLuong = tbMaLuong.Text;
                if (IsMaLuongExists(maLuong) && maLuong != dgvDSLuong.Rows[editedRowIndex].Cells["SalaryID"].Value.ToString())
                {
                    MessageBox.Show("Mã Lương đã tồn tại");
                    return;
                }
                string maNhanVien = tbMaNhanVien.Text;
                string tongLuongNhan = tbTongLuong.Text;
                string tongNgayLuong = cbTongNgayLuong.Text;

                string connString = @"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True";

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Salaries SET SalaryID = @SalaryID, EmployeeID = @EmployeeID, SalaryAmount = @SalaryAmount, SalaryDate = @SalaryDate  WHERE SalaryID = @SalaryID_Old";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@SalaryID", SqlDbType.Int).Value = maLuong;
                            command.Parameters.Add("@SalaryID_Old", SqlDbType.Int).Value = dgvDSLuong.Rows[editedRowIndex].Cells["SalaryID"].Value.ToString();
                            command.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = maNhanVien;
                            command.Parameters.Add("@SalaryAmount", SqlDbType.NVarChar).Value = tongLuongNhan;
                            command.Parameters.Add("@SalaryDate", SqlDbType.NVarChar).Value = tongNgayLuong;

                            command.ExecuteNonQuery();

                            MessageBox.Show("Dữ liệu đã được cập nhật.");
                        }
                        dataQLLuong();

                        tbMaLuong.Clear();
                        tbMaNhanVien.Clear();
                        tbTongLuong.Clear();
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
            tbMaLuong.Clear();
            tbMaNhanVien.Clear();
            tbTongLuong.Clear();
            RefreshDataGridView();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = tbTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                string query = "SELECT * FROM Salaries WHERE SalaryID LIKE @Keyword OR EmployeeID LIKE @Keyword ";
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

                                dgvDSLuong.DataSource = dataTable;

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
