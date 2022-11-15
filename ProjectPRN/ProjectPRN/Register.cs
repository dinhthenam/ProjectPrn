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
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;

namespace ProjectPRN
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                using (MyOrderContext context = new MyOrderContext())
                {
                    TblUser tkh = new TblUser()
                    {
                        Username = txtUser.Text,
                        Pass = int.Parse(txtPass.Text),
                    };

                    context.TblUsers.Add(tkh);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Add success");
                        Login login = new Login();
                        login.GetMessage(txtUser.Text, txtPass.Text);
                        login.Show();
                        this.Hide();
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Add error" + ex.Message);
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
