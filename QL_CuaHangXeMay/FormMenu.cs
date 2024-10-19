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
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock= DockStyle.Fill;
            panel_body.Controls.Add(childForm);
            panel_body.Tag  = childForm;
            childForm.BringToFront();
            childForm.Show();
            nameform.Text = childForm.Text;
        }

        private void btnFormKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormKhachHang());
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            date.Text ="Ngày: "+ DateTime.Now.ToString();
        }

        private void btnFormXe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormXe());
        }

        private void btnFormHangXe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormHangXe());
        }

        private void btnFormHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTaoHoaDon());
        }

        private void btnFormNV_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormNhanVien());
        }

        private void btnFormThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormThongKe());
        }

        private void btn_FormTrangChu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTrangChu());
        }
    }
}
