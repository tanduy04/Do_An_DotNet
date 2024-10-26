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
using System.Xml.Linq;

namespace QL_CuaHangXeMay
{
    public partial class FormKhachHang : Form
    {
        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm ...")
            {
                txtSearch.Text = "";

            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm ...";
            }
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            DBConnect DB = new DBConnect();
            string chuoitruyvan = "select * from KhachHang";
            DataTable dt = DB.getDataTable(chuoitruyvan);
            dataGridViewKH.DataSource = dt;
            
            


        }
        private void LoadHD(int MaKH)
        {
            // Chuỗi kết nối tới SQL Server

            DBConnect db = new DBConnect();
            string query = "SELECT * FROM HoaDon WHERE MaKH = " + MaKH + "";
            DataTable dt = db.getDataTable(query);
            dataGridViewHD.DataSource = dt;
            

        }

        private void dataGridViewKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy ID của khách hàng từ cột đầu tiên (ví dụ là CustomerID)
                int MaKH = Convert.ToInt32(dataGridViewKH.Rows[e.RowIndex].Cells["MaKH"].Value);
                if (e.RowIndex >= 0)
                {
                    // Lấy hàng hiện tại
                    DataGridViewRow row = dataGridViewKH.Rows[e.RowIndex];

                    // Hiển thị dữ liệu lên các TextBox và DateTimePicker
                    txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
                    txtTen.Text = row.Cells["HoTenKH"].Value.ToString();
                    txtSDT.Text = row.Cells["SoDienThoaiKH"].Value.ToString();
                    txtMail.Text = row.Cells["EmailKH"].Value.ToString();
                    txtdiachi.Text = row.Cells["DiaChi"].Value.ToString();

                    // Hiển thị ngày sinh lên DateTimePicker
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["NgaySinhKH"].Value);
                    // Gọi hàm để lấy danh sách hóa đơn của khách hàng
                    LoadHD(MaKH);
                }
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string sdt = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(sdt))
                {
                    try
                    {
                        DBConnect DB = new DBConnect();
                        string chuoitruyvan = "select * from KhachHang where SoDienThoaiKH =" + sdt + "";
                        DataTable dt = DB.getDataTable(chuoitruyvan);
                        dataGridViewKH.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tìm kiếm " + ex.Message);
                    }


                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại");
                }

            }
        }
        bool KiemTra(string ten, string sdt, string email)
        {
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.");
                return false;
            }
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.");
                return false;
            }
            if (sdt.Length != 10 && sdt.Length != 11)
            {
                MessageBox.Show("Số điện thoại phải có 10 hoặc 11 số.");
                return false;
            }
            if (!sdt.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa ký tự số.");
                return false;
            }
            if (!string.IsNullOrEmpty(email) && !email.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ. Email phải chứa ký tự '@'.");
                return false;
            }
            return true;
        }



        void LoadFormKhachHang()
        {
            DBConnect db = new DBConnect();
            string chuoitruyvan = "select * from KhachHang";
            DataTable dt = db.getDataTable(chuoitruyvan);
            dataGridViewKH.DataSource = dt;
            txtTen.Clear();
            txtSDT.Clear();
            txtMail.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string ten = txtTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtMail.Text.Trim();
            DateTime ngaysinh = dateTimePicker1.Value;
            string diachi = txtdiachi.Text.Trim();
            if (KiemTra(ten, sdt, email) == false)
            {
                return;
            }
            else
            {
                try
                {
                    DBConnect db = new DBConnect();
                    string insert = "INSERT INTO KhachHang VALUES (N'" + ten + "','" + ngaysinh + "','" + sdt + "','" + email + "',N'" + diachi + "')";
                    int kq = db.getNonQuery(insert);
                    if (kq > 0)
                    {
                        MessageBox.Show("Thêm khách hàng thành công");
                        LoadFormKhachHang();
                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống" + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string hoTenKH = txtTen.Text.Trim();
            string soDienThoaiKH = txtSDT.Text.Trim();
            DateTime ngaySinhKH = dateTimePicker1.Value;
            string emailKH = txtMail.Text.Trim();
            string diaChi = txtdiachi.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần chỉnh sửa");
                return;
            }
            if (KiemTra(hoTenKH, soDienThoaiKH, emailKH) == false)
            {
                return;
            }
            else
            {
                try
                {
                    DBConnect db = new DBConnect();
                    string edit = "UPDATE KhachHang SET HoTenKH=N'" + hoTenKH + "',NgaySinhKH='" + ngaySinhKH + "',SoDienThoaiKH='" + soDienThoaiKH + "',EmailKH='" + emailKH + "',DiaChi=N'" + diaChi + "' where MaKH=" + maKH + "";
                    int kq = db.getNonQuery(edit);
                    if (kq > 0)
                    {
                        MessageBox.Show("Sửa thành công");
                        LoadFormKhachHang();
                    }
                    else
                    {
                        MessageBox.Show("Sửa không thành công");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống" + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            // Kiểm tra nếu có hàng nào được chọn
            if (!string.IsNullOrEmpty(txtMaKH.Text))
            {
                // Lấy mã khách hàng từ hàng đã chọn
                string maKH = txtMaKH.Text;
                string query = "SELECT COUNT(*) FROM HoaDon WHERE MaKH = "+maKH+"";
                // Kiểm tra xem khách hàng có tồn tại trong bảng Hóa Đơn không
                if (db.getScalar(query)>0)
                {
                    // Thông báo người dùng xóa hóa đơn trước
                    MessageBox.Show("Vui lòng xóa những hóa đơn liên quan của khách hàng trước khi xóa.",
                                    "Không thể xóa khách hàng",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
                else
                {
                    // Xác nhận xóa khách hàng
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?",
                                                          "Xác nhận xóa",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Gọi hàm xóa khách hàng trong cơ sở dữ liệu
                        string delete = "DELETE FROM KhachHang WHERE MaKH =" + maKH + "";
                        if(db.getNonQuery(delete)>0)
                        // Cập nhật lại DataGridView                      
                        MessageBox.Show("Xóa khách hàng thành công!");
                        LoadFormKhachHang();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.");
            }
        }
    }
}
