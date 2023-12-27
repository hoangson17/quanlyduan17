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
    public partial class ChiTietNH : Form
    {
        public string SoPN { get; set; }

        public ChiTietNH()
        {
            InitializeComponent();
        }
        Modify modify;
        chitietnh1 chiTietnh;
        private void ChiTietNH_Load(object sender, EventArgs e)
        {
            modify = new Modify();
            try
            {
                guna2DataGridView1.DataSource = modify.getAllChitietnh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show(this.SoPN);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string mamh = this.guna2TextBox1.Text;
            int soluongnhap, gianhap, tongtien;

            if (int.TryParse(this.guna2TextBox2.Text, out soluongnhap) &&
                int.TryParse(this.guna2TextBox3.Text, out gianhap) &&
                int.TryParse(this.guna2TextBox4.Text, out tongtien))
            {
                string sopn = this.guna2TextBox5.Text;
                chiTietnh = new chitietnh1(mamh, soluongnhap, gianhap, tongtien, sopn);

                if (modify.insertChitietnh(chiTietnh))
                {
                    guna2DataGridView1.DataSource = modify.getAllChitietnh();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string mamh = this.guna2TextBox1.Text;
            int soluongnhap, gianhap, tongtien;

            if (int.TryParse(this.guna2TextBox2.Text, out soluongnhap) &&
                int.TryParse(this.guna2TextBox3.Text, out gianhap) &&
                int.TryParse(this.guna2TextBox4.Text, out tongtien))
            {
                string sopn = this.guna2TextBox5.Text;
                chiTietnh = new chitietnh1(mamh, soluongnhap, gianhap, tongtien, sopn);

                if (modify.updateChitietnh(chiTietnh))
                {
                    guna2DataGridView1.DataSource = modify.getAllChitietnh();
                }
                else
                {
                    MessageBox.Show("Lỗi " + "không sửa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lỗi " + "Nhập số nguyên cho Số lượng bán, Đơn giá và Tổng tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    // Lấy giá trị của cột đầu tiên (giả sử đó là cột ID) từ dòng được chọn
                    string id = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                    // Xác nhận xóa bằng MessageBox trước khi tiến hành xóa
                    DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa phiếu có số : {id} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Thực hiện xóa và kiểm tra kết quả
                        if (modify.deleteChitietpn(id))
                        {
                            guna2DataGridView1.DataSource = modify.getAllChitietnh();
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
