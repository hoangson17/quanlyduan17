using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppQuanly
{
    public partial class ChiTietHD : Form
    {
        public ChiTietHD()
        {
            InitializeComponent();
        }
        Modify modify;
        chitiethd1 chiTiethd;
        private void ChiTietHD_Load(object sender, EventArgs e)
        {
            modify = new Modify();
            try
            {
                guna2DataGridView1.DataSource = modify.getAllChitiethd();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button_them_Click(object sender, EventArgs e)
        {
            string mamh = this.guna2TextBox_mamh.Text;
            int soluongban, dongia, tongtien;

            if (int.TryParse(this.guna2TextBox2.Text, out soluongban) &&
                int.TryParse(this.guna2TextBox4.Text, out dongia) &&
                int.TryParse(this.guna2TextBox3.Text, out tongtien))
            {
                string mahd = this.guna2TextBox5.Text;
                chiTiethd = new chitiethd1(mamh, soluongban, dongia, tongtien, mahd);

                if (modify.insertChitiethd(chiTiethd))
                {
                    guna2DataGridView1.DataSource = modify.getAllChitiethd();
                }
                else
                {
                    MessageBox.Show("Lỗi " + "không thêm vào được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lỗi " + "Nhập số nguyên cho Số lượng bán, Đơn giá và Tổng tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2Button_sua_Click(object sender, EventArgs e)
        {
            string mamh = this.guna2TextBox_mamh.Text;
            int soluongban = int.Parse(this.guna2TextBox2.Text);
            int dongia = int.Parse(this.guna2TextBox4.Text);
            int tongtien = int.Parse(this.guna2TextBox3.Text);
            string mahd = this.guna2TextBox5.Text;
            chiTiethd = new chitiethd1(mamh, soluongban, dongia, tongtien, mahd);

            if (modify.updateChitiethd(chiTiethd))
            {
                guna2DataGridView1.DataSource = modify.getAllChitiethd();
            }
            else
            {
                MessageBox.Show("Lỗi " + "không cập nhật được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    // Lấy giá trị của cột đầu tiên (giả sử đó là cột ID) từ dòng được chọn
                    string id = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                    // Xác nhận xóa bằng MessageBox trước khi tiến hành xóa
                    DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn có số : {id} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Thực hiện xóa và kiểm tra kết quả
                        if (modify.deleteChitiethd(id))
                        {
                            guna2DataGridView1.DataSource = modify.getAllChitiethd();
                            MessageBox.Show("Xóa  thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không xóa được . Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
