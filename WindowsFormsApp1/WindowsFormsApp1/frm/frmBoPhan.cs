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
    public partial class frmBoPhan : Form
    {
        public frmBoPhan()
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
                string query = "SELECT * FROM Departments ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Cập nhật bảng dgvQLS bằng dữ liệu mới
                        dgvDanhSachBoPhan.DataSource = dataTable;
                    }
                }
            }
        }
        private void dataQLBoPhan()
        {
            conn.Open();
            string sql = "SELECT * FROM Departments";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dgvDanhSachBoPhan.DataSource = dt;
        }
        
        private void frmBoPhan_Load(object sender, EventArgs e)
        {
            dataQLBoPhan();
        }
        private bool IsMaBoPhanExists(string maBoPhan)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Departments WHERE DepartmentID = @DepartmentID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@DepartmentID", maBoPhan);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maBoPhan = tbIDBoPhan.Text;
            if (IsMaBoPhanExists(maBoPhan))
            {
                MessageBox.Show("Mã bộ phận đã tồn tại!");
                return;
            }
            string tenBoPhan = cbTenBoPhan.Text;

            string query = "INSERT INTO Departments (DepartmentID, DepartmentName) VALUES (@DepartmentID, @DepartmentName)";

            SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True");
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@DepartmentID", maBoPhan);
                cmd.Parameters.AddWithValue("@DepartmentName", tenBoPhan);    

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
            if (dgvDanhSachBoPhan.SelectedRows.Count > 0)
            {
                string maBoPhan = dgvDanhSachBoPhan.SelectedRows[0].Cells["DepartmentID"].Value.ToString();

                string query = "DELETE FROM Departments WHERE DepartmentID = @DepartmentID";
                using (SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentID", maBoPhan);
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
                if (dgvDanhSachBoPhan.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDanhSachBoPhan.SelectedRows[0];
                    tbIDBoPhan.Text = selectedRow.Cells["DepartmentID"].Value.ToString();
                    cbTenBoPhan.Text = selectedRow.Cells["DepartmentName"].Value.ToString();
                    
                    isEditing = true;
                    editedRowIndex = selectedRow.Index;
                    btnThem.Enabled = false;
                    btnSua.Text = "Lưu";
                }
            }
            else
            {
                string maBoPhan = tbIDBoPhan.Text;
                if (IsMaBoPhanExists(maBoPhan) && maBoPhan != dgvDanhSachBoPhan.Rows[editedRowIndex].Cells["DepartmentID"].Value.ToString())
                {
                    MessageBox.Show("Mã bộ phận đã tồn tại");
                    return;
                }
                string tenBoPhan = cbTenBoPhan.Text;
                
                string connString = @"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True";

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Departments SET DepartmentID = @DepartmentID, DepartmentName = @DepartmentName WHERE DepartmentID = @DepartmentID_Old";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = maBoPhan;
                            command.Parameters.Add("@DepartmentID_Old", SqlDbType.Int).Value = dgvDanhSachBoPhan.Rows[editedRowIndex].Cells["DepartmentID"].Value.ToString();
                            command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = tenBoPhan;
                           
                            command.ExecuteNonQuery();

                            MessageBox.Show("Dữ liệu đã được cập nhật.");
                        }
                        dataQLBoPhan();
                        
                        tbIDBoPhan.Clear();
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
                string query = "SELECT * FROM Departments WHERE DepartmentID LIKE @Keyword OR DepartmentName LIKE @Keyword ";
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

                                dgvDanhSachBoPhan.DataSource = dataTable;

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
            tbIDBoPhan.Clear();
            RefreshDataGridView();
        }
    }
}
