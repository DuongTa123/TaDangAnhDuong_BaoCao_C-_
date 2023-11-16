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
    public partial class frmYCNghiPhep : Form
    {
        public frmYCNghiPhep()
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
                string query = "SELECT * FROM LeaveRequests ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Cập nhật bảng dgvQLS bằng dữ liệu mới
                        dgvDSNghiPhep.DataSource = dataTable;
                    }
                }
            }
        }
        private void dataQLNghiPhep()
        {
            conn.Open();
            string sql = "SELECT * FROM LeaveRequests";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dgvDSNghiPhep.DataSource = dt;
        }
        private void frmYCNghiPhep_Load(object sender, EventArgs e)
        {
            dataQLNghiPhep();
        }
        private bool IsMaNghiPhepExists(string maNghiPhep)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM LeaveRequests WHERE LeaveRequestID = @LeaveRequestID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@LeaveRequestID", maNghiPhep);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNghiPhep = tbMaNghiPhep.Text;
            if (IsMaNghiPhepExists(maNghiPhep))
            {
                MessageBox.Show("Mã Lương đã tồn tại!");
                return;
            }
            string maNhanVien = tbMaNhanVien.Text;
            DateTime tuNgay = dtpTuNgay.Value;
            DateTime denNgay = dtpDenNgay.Value;
            string liDo = tbLiDo.Text;
            string trangThai = cbTrangThai.Text;

            string query = "INSERT INTO LeaveRequests (LeaveRequestID, EmployeeID, LeaveStartDate, LeaveEndDate, LeaveReason, Status ) VALUES (@LeaveRequestID, @EmployeeID, @LeaveStartDate, @LeaveEndDate, @LeaveReason, @Status)";

            SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True");
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LeaveRequestID", maNghiPhep);
                cmd.Parameters.AddWithValue("@EmployeeID", maNhanVien);
                cmd.Parameters.AddWithValue("@LeaveStartDate", tuNgay);
                cmd.Parameters.AddWithValue("@LeaveEndDate", denNgay);
                cmd.Parameters.AddWithValue("@LeaveReason", liDo);
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
            if (dgvDSNghiPhep.SelectedRows.Count > 0)
            {
                string maNghiPhep = dgvDSNghiPhep.SelectedRows[0].Cells["LeaveRequestID"].Value.ToString();

                string query = "DELETE FROM LeaveRequests WHERE LeaveRequestID = @LeaveRequestID";
                using (SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LeaveRequestID", maNghiPhep);
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
                if (dgvDSNghiPhep.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSNghiPhep.SelectedRows[0];
                    tbMaNghiPhep.Text = selectedRow.Cells["LeaveRequestID"].Value.ToString();
                    tbMaNhanVien.Text = selectedRow.Cells["EmployeeID"].Value.ToString();
                    dtpTuNgay.Text = selectedRow.Cells["LeaveStartDate"].Value.ToString();
                    dtpDenNgay.Text = selectedRow.Cells["LeaveEndDate"].Value.ToString();
                    tbLiDo.Text = selectedRow.Cells["LeaveReason"].Value.ToString();
                    cbTrangThai.Text = selectedRow.Cells["Status"].Value.ToString();

                    isEditing = true;
                    editedRowIndex = selectedRow.Index;
                    btnThem.Enabled = false;
                    btnSua.Text = "Lưu";
                }
            }
            else
            {
                string maNghiPhep = tbMaNghiPhep.Text;
                if (IsMaNghiPhepExists(maNghiPhep) && maNghiPhep != dgvDSNghiPhep.Rows[editedRowIndex].Cells["LeaveRequestID"].Value.ToString())
                {
                    MessageBox.Show("Mã Nghỉ Phép đã tồn tại");
                    return;
                }
                string maNhanVien = tbMaNhanVien.Text;
                DateTime tuNgay = dtpTuNgay.Value;
                DateTime denNgay = dtpDenNgay.Value;
                string liDo = tbLiDo.Text;
                string trangThai = cbTrangThai.Text;

                string connString = @"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True";

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE LeaveRequests SET LeaveRequestID = @LeaveRequestID, EmployeeID = @EmployeeID, LeaveStartDate = @LeaveStartDate, LeaveEndDate = @LeaveEndDate, LeaveReason= @LeaveReason, Status= @Status  WHERE LeaveRequestID = @LeaveRequestID_Old";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@LeaveRequestID", SqlDbType.Int).Value = maNghiPhep;
                            command.Parameters.Add("@LeaveRequestID_Old", SqlDbType.Int).Value = dgvDSNghiPhep.Rows[editedRowIndex].Cells["LeaveRequestID"].Value.ToString();
                            command.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = maNhanVien;
                            command.Parameters.Add("@LeaveStartDate", SqlDbType.NVarChar).Value = tuNgay;
                            command.Parameters.Add("@LeaveEndDate", SqlDbType.NVarChar).Value = denNgay;
                            command.Parameters.Add("@LeaveReason", SqlDbType.NVarChar).Value = liDo;
                            command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = trangThai;

                            command.ExecuteNonQuery();

                            MessageBox.Show("Dữ liệu đã được cập nhật.");
                        }
                        dataQLNghiPhep();

                        tbMaNghiPhep.Clear();
                        tbMaNhanVien.Clear();
                        tbLiDo.Clear();
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
            tbMaNghiPhep.Clear();
            tbMaNhanVien.Clear();
            tbLiDo.Clear();
            RefreshDataGridView();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = tbTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                string query = "SELECT * FROM LeaveRequests WHERE LeaveRequestID LIKE @Keyword OR EmployeeID LIKE @Keyword OR Status LIKE @Keyword ";
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

                                dgvDSNghiPhep.DataSource = dataTable;

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
