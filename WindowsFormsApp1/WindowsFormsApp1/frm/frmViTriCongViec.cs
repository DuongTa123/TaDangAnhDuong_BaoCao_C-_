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
    public partial class frmViTriCongViec : Form
    {
        public frmViTriCongViec()
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
                string query = "SELECT * FROM Positions ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Cập nhật bảng dgvQLS bằng dữ liệu mới
                        dgvDSViTriCongViec.DataSource = dataTable;
                    }
                }
            }
        }
        private void dataQLViTriCongViec()
        {
            conn.Open();
            string sql = "SELECT * FROM Positions";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dgvDSViTriCongViec.DataSource = dt;

        }

        private void frmViTriCongViec_Load(object sender, EventArgs e)
        {
            dataQLViTriCongViec();

        }
        private bool IsMaCongViecExists(string maVTriCongViec)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Positions WHERE PositionID = @PositionID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PositionID", maVTriCongViec);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maVTriCongViec = tbIDCongViec.Text;
            if (IsMaCongViecExists(maVTriCongViec))
            {
                MessageBox.Show("Mã bộ phận đã tồn tại!");
                return;
            }
            string tenVTriCViec = cbTenCongViec.Text;

            string query = "INSERT INTO Positions (PositionID, PositionName) VALUES (@PositionID, @PositionName)";

            SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True");
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PositionID", maVTriCongViec);
                cmd.Parameters.AddWithValue("@PositionName", tenVTriCViec);

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
            if (dgvDSViTriCongViec.SelectedRows.Count > 0)
            {
                string maVTriCongViec = dgvDSViTriCongViec.SelectedRows[0].Cells["PositionID"].Value.ToString();

                string query = "DELETE FROM Positions WHERE PositionID = @PositionID";
                using (SqlConnection conn = new SqlConnection(@"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PositionID", maVTriCongViec);
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
                if (dgvDSViTriCongViec.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSViTriCongViec.SelectedRows[0];
                    tbIDCongViec.Text = selectedRow.Cells["PositionID"].Value.ToString();
                    cbTenCongViec.Text = selectedRow.Cells["PositionName"].Value.ToString();

                    isEditing = true;
                    editedRowIndex = selectedRow.Index;
                    btnThem.Enabled = false;
                    btnSua.Text = "Lưu";
                }
            }
            else
            {
                string maVTriCongViec = tbIDCongViec.Text;
                if (IsMaCongViecExists(maVTriCongViec) && maVTriCongViec != dgvDSViTriCongViec.Rows[editedRowIndex].Cells["PositionID"].Value.ToString())
                {
                    MessageBox.Show("Mã vị trí công việc đã tồn tại");
                    return;
                }
                string tenVtriCViec = cbTenCongViec.Text;

                string connString = @"Data Source =.; Initial Catalog = QuanLiNhanVien; Integrated Security = True";

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Positions SET PositionID = @PositionID, PositionName = @PositionName WHERE PositionID = @PositionID_Old";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@PositionID", SqlDbType.Int).Value = maVTriCongViec;
                            command.Parameters.Add("@PositionID_Old", SqlDbType.Int).Value = dgvDSViTriCongViec.Rows[editedRowIndex].Cells["PositionID"].Value.ToString();
                            command.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = tenVtriCViec;

                            command.ExecuteNonQuery();

                            MessageBox.Show("Dữ liệu đã được cập nhật.");
                        }
                        dataQLViTriCongViec();

                        tbIDCongViec.Clear();
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
            tbIDCongViec.Clear();
            RefreshDataGridView();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = tbTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                string query = "SELECT * FROM Positions WHERE PositionID LIKE @Keyword OR PositionName LIKE @Keyword ";
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

                                dgvDSViTriCongViec.DataSource = dataTable;

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