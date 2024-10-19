using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if(txtSearch.Text == "Tìm kiếm ...")
            {
                txtSearch.Text = "";

            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm ...";
            }
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            DBConnect DB = new DBConnect();
            string chuoitruyvan = "select * from KhachHang";
            DataTable dt = DB.getDataTable(chuoitruyvan);
            dataGridView1.DataSource = dt;
        }
    }
}
