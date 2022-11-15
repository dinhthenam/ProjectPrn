using Microsoft.EntityFrameworkCore.Diagnostics;
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
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjectPRN
{
    public partial class FormSale : Form
    {
        public FormSale()
        {
            InitializeComponent();
        }
        MyOrderContext context = new MyOrderContext();

        private void FormSale_Load(object sender, EventArgs e)
        {
            AutoComplete();
            btnBuy.Enabled = false;
            btnIn.Enabled = false;
            btnOut.Enabled = false;
            Loaddata();
            
        }

        private void Loaddata()
        {
            var data = context.TblMatHangs.ToList();
            cbMatHang.DataSource = data;
            cbMatHang.DisplayMember = "TenHang";
            cbMatHang.ValueMember = "MaHang";
            cbMatHang.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbMatHang.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        private List<TblKhachHang> KhachHang(string code)
        {
            return context.TblKhachHangs.Where(item => item.MaKh.Contains(code)).ToList();
        }
            private List<TblKhachHang> KhachHang2(string name)
            {
                return context.TblKhachHangs.Where(item => item.TenKh.Contains(name)).ToList();
            }

            private void AutoComplete()
        {
            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            List<TblKhachHang> code1 = KhachHang(txtCodeCustomer.Text);
            List<TblKhachHang> name1 = KhachHang2(txtNameCustomer.Text);
            foreach(TblKhachHang k in code1)
            {
                auto.Add(k.MaKh);
            }
            foreach(TblKhachHang k2 in name1)
            {
                auto.Add(k2.TenKh);
            }
            txtCodeCustomer.AutoCompleteCustomSource = auto;
            txtCodeCustomer.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtCodeCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNameCustomer.AutoCompleteCustomSource = auto;
            txtNameCustomer.AutoCompleteMode= AutoCompleteMode.Suggest;
            txtNameCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
        }
        private void check(string code)
        {
            var data = context.TblKhachHangs.Where(item => item.MaKh==code).ToList();
            List<TblHoadon> kh = context.TblHoadons.Where(item => item.MaKh == code).ToList();
            foreach (TblKhachHang k in data)
            {
                txtNameCustomer.Text = k.TenKh;
                txtAddress.Text = k.Diachi;
                foreach(TblHoadon m in kh)
                {
                    txtMaHoaDon.Text = m.MaHd.ToString();
                }
            }
        }
        private void check2(string name)
        {
            var data = context.TblKhachHangs.Where(item => item.TenKh == name).ToList();
            foreach (TblKhachHang k in data)
            {
                txtCodeCustomer.Text = k.MaKh;
                txtAddress.Text = k.Diachi;
                //txtMaHoaDon = k.TblHoadons.Where(item=>item.MaKh.Contains();
            }
        }
        private void check3(string name)
        {
            var data = context.TblMatHangs.Where(item => item.TenHang == name).ToList();
            foreach(TblMatHang m in data)
            {
                txtPrice.Text = m.Gia.ToString();
            }
        }

        private void txtCodeLeave(object sender, EventArgs e)
        {
            check(txtCodeCustomer.Text);
            
        }

        private void txtNameleave(object sender, EventArgs e)
        {
            check2(txtNameCustomer.Text);
        }

        private void cbMaChange(object sender, EventArgs e)
        {
            check3(cbMatHang.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCodeCustomer.Clear();
            txtAddress.Clear();
            txtNameCustomer.Clear();
            txtPrice.Clear();
            txtNumbers.Clear();
            txtDate.Clear();
            txtMaHoaDon.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        decimal ctKh = 0;

        private void btnBuy_Click(object sender, EventArgs e)
        {
            try
            {
                var data1 = context.TblChiTietHds.ToList();
                if (string.IsNullOrEmpty(txtCodeCustomer.Text.Trim()))
                {
                    MessageBox.Show("Vui long dien MaKh");
                }
                else if (string.IsNullOrEmpty(txtMaHoaDon.Text.Trim()))
                {
                    MessageBox.Show("Vui long dien Ma HD");
                }
                else if (string.IsNullOrEmpty(txtDate.Text.Trim()))
                {
                    MessageBox.Show("Vui long dien NgayHD");
                }
                else
                {
                    TblKhachHang kh = new TblKhachHang
                    {
                        MaKh = txtCodeCustomer.Text,
                        TenKh = txtNameCustomer.Text,
                        Diachi = txtAddress.Text,
                        Gt = true,
                        NgaySinh = DateTime.Now
                    };

                    context.Add(kh);
                    TblHoadon hd = new TblHoadon
                    {
                        MaHd = decimal.Parse(txtMaHoaDon.Text),
                        MaKh = txtCodeCustomer.Text,
                        NgayHd = Convert.ToDateTime(txtDate.Text)
                    };

                    context.Add(hd);



                    if (context.SaveChanges() > 0)
                    {
                        dgProduct.Visible = true;
                        dgProduct.DataSource = data1;
                        btnIn.Enabled = true;
                        btnOut.Enabled = true;
                        txtNumbers.Enabled = true;
                        txtPrice.Enabled = true;
                        ctKh = context.TblChiTietHds.Max(a => a.MaChiTietHd) + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add order error " + ex.Message);
            }
        }
    }
}
