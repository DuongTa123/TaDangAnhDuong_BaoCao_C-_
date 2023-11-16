using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1.frm
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
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
                string query = "SELECT * FROM Employees ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Cập nhật bảng dgvQLS bằng dữ liệu mới
                        dgvNhanVien.DataSource = dataTable;
                    }
                }
            }
        }

        private void dataQLNhanVien()
        {
            conn.Open();
            string sql = "SELECT * FROM Employees";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dgvNhanVien.DataSource = dt;
        }
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dataQLNhanVien();
        }


        private bool IsMaNhanVienExists(string maNhanVien)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @EmployeeID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", maNhanVien);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNhanVien = tbIDNhanVien.Text;
            if (IsMaNhanVienExists(maNhanVien))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại!");
                return;
            }
            string hoTenNhanVien = tbHoNhanVien.Text;
            string tenNhanVien = tbTenNhanVien.Text;
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = cbGioiTinh.Text;
            string email = tbEmail.Text;
            string soDienThoai = tbDienThoai.Text;
            string diaChi = tbDiaChi.Text;
            int idBoPhan = Convert.ToInt32(cbIDBoPhan.Text);

            string query = "INSERT INTO Employees (EmployeeID, FirstName, LastName, DateOfBirth, Gender, Email, Phone, Address, DepartmentID) " +
                                          "VALUES (@EmployeeID, @FirstName, @LastName, @DateOfBirth, @Gender, @Email, @Phone, @Address, @DepartmentID)";

            SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True");
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@EmployeeID", maNhanVien);
                cmd.Parameters.AddWithValue("@FirstName", hoTenNhanVien);
                cmd.Parameters.AddWithValue("@LastName", tenNhanVien);
                cmd.Parameters.AddWithValue("@DateOfBirth", ngaySinh);
                cmd.Parameters.AddWithValue("@Gender", gioiTinh);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", soDienThoai);
                cmd.Parameters.AddWithValue("@Address", diaChi);
                cmd.Parameters.AddWithValue("@DepartmentID", idBoPhan);

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
        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                if (dgvNhanVien.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvNhanVien.SelectedRows[0];
                    tbIDNhanVien.Text = selectedRow.Cells["EmployeeID"].Value.ToString();
                    tbHoNhanVien.Text = selectedRow.Cells["FirstName"].Value.ToString();
                    tbTenNhanVien.Text = selectedRow.Cells["LastName"].Value.ToString();
                    dtpNgaySinh.Text = selectedRow.Cells["DateOfBirth"].Value.ToString();
                    cbGioiTinh.Text = selectedRow.Cells["Gender"].Value.ToString();
                    tbEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                    tbDienThoai.Text = selectedRow.Cells["Phone"].Value.ToString();
                    tbDiaChi.Text = selectedRow.Cells["Address"].Value.ToString();
                    cbIDBoPhan.Text = selectedRow.Cells["DepartmentID"].Value.ToString();
                    isEditing = true;
                    editedRowIndex = selectedRow.Index;
                    btnThem.Enabled = false;
                    btnSua.Text = "Lưu";
                }
            }
            else
            {
                string maNhanVien = tbIDNhanVien.Text;
                if (IsMaNhanVienExists(maNhanVien) && maNhanVien != dgvNhanVien.Rows[editedRowIndex].Cells["EmployeeID"].Value.ToString())
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại");
                    return;
                }
                string hoTenNhanVien = tbHoNhanVien.Text;
                string tenNhanVien = tbTenNhanVien.Text;
                DateTime ngaySinh = dtpNgaySinh.Value;
                string gioiTinh = cbGioiTinh.Text;
                string email = tbEmail.Text;
                string soDienThoai = tbDienThoai.Text;
                string diaChi = tbDiaChi.Text;
                int idBoPhan = Convert.ToInt32(cbIDBoPhan.Text);

                string connString = @"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True";

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Employees SET EmployeeID = @EmployeeID, FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, Email = @Email, Phone = @Phone, Address = @Address , DepartmentID = @DepartmentID  WHERE EmployeeID = @EmployeeID_Old";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = maNhanVien;
                            command.Parameters.Add("@EmployeeID_Old", SqlDbType.Int).Value = dgvNhanVien.Rows[editedRowIndex].Cells["EmployeeID"].Value.ToString();
                            command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = hoTenNhanVien;
                            command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = tenNhanVien;
                            command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = ngaySinh;
                            command.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = gioiTinh;
                            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                            command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = soDienThoai;
                            command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = diaChi;
                            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = idBoPhan;
                            command.ExecuteNonQuery();

                            MessageBox.Show("Dữ liệu đã được cập nhật.");
                        }
                        dataQLNhanVien();
                        tbIDNhanVien.Clear();
                        tbHoNhanVien.Clear();
                        tbTenNhanVien.Clear();
                        tbEmail.Clear();
                        dtpNgaySinh.Value = DateTime.Now;
                        tbDienThoai.Clear();
                        tbDiaChi.Clear();
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                string maNhanVien = dgvNhanVien.SelectedRows[0].Cells["EmployeeID"].Value.ToString();

                string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                using (SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", maNhanVien);
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = tbTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                string query = "SELECT * FROM Employees WHERE EmployeeID LIKE @Keyword OR FirstName LIKE @Keyword OR LastName LIKE @Keyword";
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

                                dgvNhanVien.DataSource = dataTable;

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
            tbIDNhanVien.Clear();
            tbHoNhanVien.Clear();
            tbTenNhanVien.Clear();
            tbEmail.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            tbDienThoai.Clear();
            tbDiaChi.Clear();
            RefreshDataGridView(); 
        }
    }
}
