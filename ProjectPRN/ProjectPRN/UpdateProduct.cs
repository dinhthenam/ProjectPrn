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

namespace ProjectPRN
{
    public partial class UpdateProduct : Form
    {
        public UpdateProduct()
        {
            InitializeComponent();
        }
        MyOrderContext context = new MyOrderContext();

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            
            btnAdd.Enabled = true;
            txtCode.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtCode.ReadOnly = false;
            txtName.ReadOnly = false;
            txtPrice.ReadOnly = false;
            cbDVT.Enabled = true;
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                using (MyOrderContext context = new MyOrderContext())
                {
                    TblMatHang tkh = new TblMatHang();
                    tkh.MaHang = txtCode.Text.ToUpper();
                    tkh.TenHang = txtName.Text;
                    tkh.Dvt = (string)cbDVT.SelectedValue;
                    tkh.Gia = Int32.Parse(txtPrice.Text);

                    context.Add(tkh);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Add success");
                        LoadData();
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Add error" + ex.Message);
            }
        }

        private void btnNotify_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            txtCode.ReadOnly = false;
            txtName.ReadOnly = false;
            txtPrice.ReadOnly = false;
            cbDVT.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Tìm đối tượng sẽ Update
                TblMatHang p = context.TblMatHangs
                    .FirstOrDefault(item => item.MaHang == txtCode.Text);

                if (p != null)
                {
                    p.MaHang = txtCode.Text;
                    p.TenHang = txtName.Text;
                    p.Dvt = (string)cbDVT.SelectedValue;
                    p.Gia = float.Parse(txtPrice.Text);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Update successs");
                        LoadData();
                    }
                }
                else
                {
                    MessageBox.Show("Update fail");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error:" + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //Tìm đối tượng sẽ delete
                TblMatHang p = context.TblMatHangs
                    .FirstOrDefault(item => item.MaHang == txtCode.Text);
                if (p != null)
                {
                    context.TblMatHangs.Remove(p);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Delete successs");
                        LoadData();
                    }
                }
                else
                {
                    MessageBox.Show("Delete fail");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete error:" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateProduct_Load(object sender, EventArgs e)
        {
            LoadData();
            txtCode.ReadOnly = true;
            txtName.ReadOnly = true;
            txtPrice.ReadOnly = true;
            cbDVT.Enabled = false;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void LoadData()
        {
            var data = context.TblMatHangs.Select(item => new
            {
              MaHang = item.MaHang,
              TenHang = item.TenHang,
                Dvt = item.Dvt,
              Gia = item.Gia
              

            }).ToList();
            dgProduct.DataSource = data;
            txtCode.DataBindings.Clear();
            txtCode.DataBindings.Add("Text", data, "MaHang");
            txtName.DataBindings.Clear();
            txtName.DataBindings.Add("Text", data, "TenHang");
            var data2 = context.TblMatHangs.ToList();
            cbDVT.DataSource = data2;
            cbDVT.DisplayMember = "Dvt";
            cbDVT.ValueMember = "MaHang";
            txtPrice.DataBindings.Clear();
            txtPrice.DataBindings.Add("Text", data, "Gia");

        }
        private int ValidateDate(string date)
        {
            try
            {
                // for US, alter to suit if splitting on hyphen, comma, etc.
                string[] dateParts = date.Split('/');

                // create new date from the parts; if this does not fail
                // the method will return true and the date is valid
                DateTime testDate = new
                    DateTime(Convert.ToInt32(dateParts[2]),
                    Convert.ToInt32(dateParts[1]),
                    Convert.ToInt32(dateParts[0]));

                return 1;
                if (testDate >= DateTime.Now)
                {
                    return 2;
                }
            }
            catch
            {
                // if a test date cannot be created, the
                // method will return false
                return 0;
            }
        }

    }
}
