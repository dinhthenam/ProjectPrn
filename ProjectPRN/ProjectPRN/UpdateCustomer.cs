using ProjectPRN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Windows.Forms.VisualStyles;

namespace ProjectPRN
{
    public partial class UpdateCustomer : Form
    {
        public UpdateCustomer()
        {
            InitializeComponent();
        }
        MyOrderContext context = new MyOrderContext();

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateCustomer_Load(object sender, EventArgs e)
        {
            LoadData();
            txtAddress.ReadOnly = true;
            txtCode.ReadOnly = true;
            txtDate.ReadOnly = true;
            txtName.ReadOnly = true;
            btnMale.Enabled = false;
            btnFemale.Enabled = false;

        }

        private void LoadData()
        {


            var data = context.TblKhachHangs.Select(
                        item => new
                        {
                            MaKh = item.MaKh,
                            TenKh = item.TenKh,
                            GioiTinh = (bool)item.Gt ? "Nam" : "Nu",
                            Diachi = item.Diachi,
                            NgaySinh = item.NgaySinh

                        }
                        ).ToList();
            dgCustomer.DataSource = data;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCode.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtDate.Clear();
            btnMale.DataBindings.Clear();
            btnFemale.DataBindings.Clear();
            txtCode.ReadOnly = false;
            txtName.ReadOnly = false;
            txtAddress.ReadOnly = false;
            txtDate.ReadOnly = false;
            btnMale.Enabled = true;
            btnFemale.Enabled = true;
            

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool gt;
            bool gender = true;
            if (btnFemale.Checked)
            {
                gender = false;
            }
            try
            {
                //tao doi tuong se add
                TblKhachHang customer = new TblKhachHang()
                {
                    MaKh = txtCode.Text,
                    TenKh = txtName.Text,
                    Diachi = txtAddress.Text,
                    Gt = gender,
                    NgaySinh = DateTime.Parse(txtDate.Text),

                };

                context.TblKhachHangs.Add(customer);
                if (context.SaveChanges() > 0)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }

        }

        private void btnNotify_Click(object sender, EventArgs e)
        {
            txtAddress.ReadOnly = false;
            txtCode.ReadOnly = false;
            txtDate.ReadOnly = false;
            txtName.ReadOnly = false;
            btnMale.Enabled = true;
            btnFemale.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool gender = true;
            if (btnFemale.Checked)
            {
                gender = false;
            }
            try
            {
                TblKhachHang p = context.TblKhachHangs.FirstOrDefault(item => item.MaKh == txtCode.Text);
                if (p != null)
                {
                    p.MaKh = txtCode.Text;
                    p.TenKh = txtName.Text;
                    p.Diachi = txtAddress.Text;
                    p.Gt = gender;
                    p.NgaySinh = DateTime.Parse(txtDate.Text);
                }
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Update successfully");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //find object to delete
                TblKhachHang p = context.TblKhachHangs.FirstOrDefault(item => item.MaKh == txtCode.Text);
                if (p != null)
                {
                    context.TblKhachHangs.Remove(p);
                    if (context.SaveChanges() > 0)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
               txtCode.Text = dgCustomer.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                txtName.Text = dgCustomer.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();

                if (dgCustomer.Rows[e.RowIndex].Cells[2].FormattedValue.ToString().Equals("Nam"))
                {
                    btnMale.Checked = true;
                }
                else
                {
                    btnFemale.Checked = true;
                }
                txtAddress.Text = dgCustomer.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                txtDate.Text = dgCustomer.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();

            }


        }
    }
}
